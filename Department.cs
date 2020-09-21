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
    /// Описывает отдел
    /// </summary>
    class Department : INotifyPropertyChanged
    {      
        private int departmentId;       
        private string name;

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
        /// Название подразделения
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


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
        /// Получение данных о должностях
        /// </summary>
        /// <returns>Спсок должностей</returns>
        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            // название процедуры
            string sqlExpression = "dbo.sp_GetDepartments";

            SqlConnection connection = new SqlConnection(DataBaseConnector.ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.Message, "Ошибка соединения", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        departments.Add(new Department()
                        {
                            DepartmentId = Convert.ToInt32(reader["id"]),
                            Name = (reader["name"].ToString())
                        });
                    }
                }
                reader.Close();
            }
            connection.Close();

            return departments;
        }
    }
}


