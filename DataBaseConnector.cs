using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WorkWithDB
{
    /// <summary>
    /// Описывает настройки взаимодействия с базой данных
    /// </summary>
    public class DataBaseConnector : INotifyPropertyChanged
    {
        private string connectingText;       

        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        //public static string ConnectionString = "Data Source=DESKTOP-IQRT15H;Initial Catalog=TestDB;Integrated Security=True";

       
        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString
        {
            get { return connectingText; }
            set
            {
                connectingText = value;
                OnPropertyChanged(nameof(ConnectionString));   
            }
        }

        /// <summary>
        /// Инициализирукт класс подключения к БД
        /// </summary>
        public DataBaseConnector()
        {
            ConnectToDBCommand = new RelayCommand(ConnectToDB);

            ConnectionString = readConnectingString();
        }


        // запись в файл
        private void SaveStringToFile(string userInput)
        {
            using (FileStream fstream = new FileStream($"ConnectingString.txt", FileMode.OpenOrCreate))
            {                
                byte[] array = System.Text.Encoding.Default.GetBytes(userInput);                
                fstream.Write(array, 0, array.Length);                
            }
        }

        public static string readConnectingString()
        {
            string connStr = string.Empty;
            try
            {
                using (FileStream fstream = File.OpenRead($"ConnectingString.txt"))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    connStr = System.Text.Encoding.Default.GetString(array);
                }
            }
            catch { }
            return connStr;
        }


        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Команда проверки подключения к базе данных
        /// </summary>
        public ICommand ConnectToDBCommand { get; set; }

        private void ConnectToDB(object sender)
        {
            SqlConnection connection = null;
            bool excHappened = false;
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка соединения", MessageBoxButton.OK, MessageBoxImage.Error);
                excHappened = true;
            }
            finally
            {
                if(connection != null) connection.Close();
            }            

            if(excHappened)
            {
                return;
            }            
                     
            SaveStringToFile(ConnectionString); 
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this)
                {
                    item.Close();
                }
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
    }
}
