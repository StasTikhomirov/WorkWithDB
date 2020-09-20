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
    /// Описывает статус сотрудника в компании
    /// </summary>
    class Status : INotifyPropertyChanged
    {
        private int statusId;
        private string name;

        /// <summary>
        /// Код  статуса
        /// </summary>
        public int StatusId
        {
            get { return statusId; }
            set
            {
                statusId = value;
                OnPropertyChanged(nameof(StatusId));
            }
        }

        /// <summary>
        /// Название 
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
        /// Получение данных о статусах
        /// </summary>
        /// <returns>Список статусов сотрудников</returns>
        public static List<Status> GetStatuses()
        {
            List<Status> statuses = new List<Status>();

            // название процедуры
            string sqlExpression = "dbo.sp_GetStatuses";

            SqlConnection connection = new SqlConnection(SQLSettings.ConnectionString);
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
                        statuses.Add(new Status()
                        {
                            StatusId = Convert.ToInt32(reader["id"]),
                            Name = (reader["name"].ToString())
                        });
                    }
                }
                reader.Close();
            }
            connection.Close();

            return statuses;
        }
    }
}
