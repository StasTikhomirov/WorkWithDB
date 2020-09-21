using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WorkWithDB
{
    class dbViewModel : DependencyObject
    {
        #region Properties

        /// <summary>
        /// Коллекция данных для отображения
        /// </summary>
        public List<Person> Persons
        {
            get { return (List<Person>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<Person>), typeof(dbViewModel), new PropertyMetadata(null));

        public List<Post> Posts
        {
            get { return (List<Post>)GetValue(PostsProperty); }
            set { SetValue(PostsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Posts.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PostsProperty =
            DependencyProperty.Register("Posts", typeof(List<Post>), typeof(dbViewModel), new PropertyMetadata(null));


        public List<Status> Statuses
        {
            get { return (List<Status>)GetValue(StatusesProperty); }
            set { SetValue(StatusesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Posts.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusesProperty =
            DependencyProperty.Register("Statuses", typeof(List<Status>), typeof(dbViewModel), new PropertyMetadata(null));

        public List<Department> Departments
        {
            get { return (List<Department>)GetValue(DepartmentsProperty); }
            set { SetValue(DepartmentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Departments.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepartmentsProperty =
            DependencyProperty.Register("Departments", typeof(List<Department>), typeof(dbViewModel), new PropertyMetadata(null));

        public List<GeneralPersonsInfo> GeneralInfo
        {
            get { return (List<GeneralPersonsInfo>)GetValue(GeneralInfoProperty); }
            set { SetValue(GeneralInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GeneralInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GeneralInfoProperty =
            DependencyProperty.Register("GeneralInfo", typeof(List<GeneralPersonsInfo>), typeof(dbViewModel), new PropertyMetadata(null));



        #endregion

        //public List<GeneralPersonsInfo> GeneralInfo;

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Получает данные
        /// Вызывает метод заполнения общего списка
        /// </summary>
        public dbViewModel()
        {
            Persons = Person.GetPersons();
            Posts = Post.GetPosts();
            Statuses = Status.GetStatuses();
            Departments = Department.GetDepartments();

            SetGeneralInfo();
        }

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Задает общий список, с информацией о сотрудниках
        /// </summary>
        private void SetGeneralInfo()
        {
            GeneralInfo = new List<GeneralPersonsInfo>();

            foreach (var person in Persons)
            {
                string io = person.FirstName.ElementAt(0) + "." + person.LastName.ElementAt(0) + ".";
               
                GeneralInfo.Add(new GeneralPersonsInfo()
                {
                    PersonId = person.PersonId,
                    Name = person.SecondName + ' ' + io,
                    DateEmploy = person.DateEmploy,
                    DateUnEmploy = person.DateUnEmploy,
                    Status = Statuses.Find(x => x.StatusId == person.StatusId).Name,
                    Department = Departments.Find(x => x.DepartmentId == person.DepartmentId).Name,
                    Post = Posts.Find(x => x.PostId == person.PostId).Name
                });
            };
        }
        //----------------------------------------------------------------------------------------------------

    }
    //----------------------------------------------------------------------------------------------------

}
