using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WorkWithDB
{
    /// <summary>
    /// Описывает сотрудника компании
    /// </summary>
    class Person : INotifyPropertyChanged
    {       
        private int personId;        
        private string secondName;        
        private string firstName;       
        private string lastName;             
        private DateTime dateEmploy;
        private DateTime dateUnEmploy;
        private int status;      
        private int departmentId;
        private int postId;

        #region Properties
        /// <summary>
        /// Id сотрудника
        /// </summary>
        public int PersonId
        {
            get { return personId; }
            set
            {
                personId = value;
                OnPropertyChanged(nameof(PersonId));
            }  
        }                
       
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged(nameof(SecondName));
            }
        }

        /// <summary>
        /// Отчество
        /// </summary>        
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        /// <summary>
        /// Дата наема
        /// </summary>
        public DateTime DateEmploy
        {
            get { return dateEmploy; }
            set
            {
                dateEmploy = value;
                OnPropertyChanged(nameof(DateEmploy));
            }
        }

        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime DateUnEmploy
        {
            get { return dateUnEmploy; }
            set
            {
                dateUnEmploy = value;
                OnPropertyChanged(nameof(DateUnEmploy));
            }
        }

        /// <summary>
        /// Статус
        /// </summary>
        public int StatusId
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(StatusId));
            }
        }


        /// <summary>
        /// Код подразделения
        /// </summary>
        public int DepartmentId
        {
            get { return departmentId; }
            set
            {
                departmentId = value;
                OnPropertyChanged(nameof(DepartmentId));
            }
        }

        /// <summary>
        ///  Код должнсти
        /// </summary>
        public int PostId
        {
            get { return postId; }
            set
            {
                postId = value;
                OnPropertyChanged(nameof(PostId));
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Обработчик собития изменения свойств класса
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Получение данных о сотрудниках 
        /// </summary>
        /// <returns>Спсок сотрудников</returns>
        public static List<Person> GetPersons()
        {
            List<Person> persons = new List<Person>();

            // название процедуры
            string sqlExpression = "dbo.sp_GetPersons";

            SqlConnection connection = new SqlConnection(SQLSettings.ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException se)
            {
                MessageBox.Show( se.Message,"Ошибка соединения",MessageBoxButton.OK,MessageBoxImage.Error);
                return null;
            }

            using (connection)
            {                
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        persons.Add(new Person()
                        {
                            PersonId = Convert.ToInt32(reader["id"]),
                            FirstName = (reader["first_name"].ToString()),
                            SecondName = (reader["second_name"].ToString()),
                            LastName = (reader["last_name"].ToString()),
                            DateEmploy = Convert.ToDateTime(reader["date_employ"]),
                            DateUnEmploy = Convert.ToDateTime(reader["date_unemploy"]),
                            StatusId = Convert.ToInt32(reader["status"]),
                            DepartmentId = Convert.ToInt32(reader["id_dep"]),
                            PostId = Convert.ToInt32(reader["id_post"]),
                        });
                    }
                }
                reader.Close();                
            }
            connection.Close();

            return persons;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------
}


