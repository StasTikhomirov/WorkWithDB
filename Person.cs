using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDB
{
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
        private int idPost;

        /// <summary>
        /// Id сотрудника
        /// </summary>
        public int PersonId
        {
            get { return personId; }
            set
            {
                personId = value;
                OnPropertyChanged("EmployeeId");
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
                OnPropertyChanged("FirstName");
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
                OnPropertyChanged("SecondName");
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
                OnPropertyChanged("LastName");
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
                OnPropertyChanged("DateEmploy");
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
                OnPropertyChanged("DateUnEmploy");
            }
        }

        /// <summary>
        /// Статус
        /// </summary>
        public int Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
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
                OnPropertyChanged("DepartmentId");
            }
        }



        public int IdPost
        {
            get { return idPost; }
            set
            {
                idPost = value;
                OnPropertyChanged("IdPost");
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
