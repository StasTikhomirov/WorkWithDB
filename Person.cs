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
