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
    }
}


