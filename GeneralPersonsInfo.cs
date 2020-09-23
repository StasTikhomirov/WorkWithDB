using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDB
{
    class GeneralPersonsInfo : INotifyPropertyChanged
    {
        private int personId;
        private string name;       
        private DateTime? dateEmploy;
        private DateTime? dateUnEmploy;
        private string status;
        private string department;
        private string post;


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
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));                 
            }
        }

        /// <summary>
        /// Дата наема
        /// </summary>
        public DateTime? DateEmploy
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
        public DateTime? DateUnEmploy
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
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }


        /// <summary>
        /// Отдел
        /// </summary>
        public string Department
        {
            get { return department; }
            set
            {
                department = value;
                OnPropertyChanged(nameof(Department));
            }
        }

        /// <summary>
        ///  Должнсть
        /// </summary>
        public string Post
        {
            get { return post; }
            set
            {
                post = value;
                OnPropertyChanged(nameof(Post));
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
    }
}
