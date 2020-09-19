using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
    }
}
