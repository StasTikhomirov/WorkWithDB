﻿using System;
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
    /// Описывает должность в компании
    /// </summary>
    class Post : INotifyPropertyChanged
    {
        private int postId;
        private string name;

        #region Properties
        /// <summary>
        /// Код должности
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
        #endregion


        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Получение данных о должностях
        /// </summary>
        /// <returns>Спсок должностей</returns>
        public static List<Post> GetPosts(string connectionString)
        {
            List<Post> posts = new List<Post>();

            // название процедуры
            string sqlExpression = "dbo.sp_GetPosts";

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.Message, "Ошибка соединения", MessageBoxButton.OK, MessageBoxImage.Error);
                return posts;
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
                        posts.Add(new Post()
                        {
                            PostId = Convert.ToInt32(reader["id"]),                            
                            Name = (reader["name"].ToString())
                        });
                    }
                }
                reader.Close();
            }
            connection.Close();

            return posts;
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

    }
    //----------------------------------------------------------------------------------------------------
}
