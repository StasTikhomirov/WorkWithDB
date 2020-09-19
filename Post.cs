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
    /// Описывает должность в компании
    /// </summary>
    class Post : INotifyPropertyChanged
    {
        private int postId;
        private string name;

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
