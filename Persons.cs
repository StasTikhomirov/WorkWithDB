using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDB
{
    class Persons : INotifyPropertyChanged
    {
        private int personId;        
        private string secondName;        
        private string firstName;       
        private string lastName;             
        private DateTime dateEmploy;
        private DateTime dateUnEmploy;
        private int status;      
        private int idDepartment;
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

        public DateTime DateEmploy
        {
            get { return dateEmploy; }
            set
            {
                dateEmploy = value;
                OnPropertyChanged("DateEmploy");
            }
        }

        public DateTime DateUnEmploy
        {
            get { return dateUnEmploy; }
            set
            {
                dateUnEmploy = value;
                OnPropertyChanged("DateUnEmploy");
            }
        }

        public int Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }


        public int IdDepartment
        {
            get { return idDepartment; }
            set
            {
                idDepartment = value;
                OnPropertyChanged("IdDepartment");
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
