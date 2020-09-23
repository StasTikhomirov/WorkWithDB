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
        private ICollectionView generalInfoCollection;
        private ObservableCollection<GeneralPersonsInfo> generalCollection;

        #region Properties

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


        public ICollectionView GeneralInfoCollection
        {
            get { return generalInfoCollection; }
            set { generalInfoCollection = value; }               
        }

        public ObservableCollection<GeneralPersonsInfo> GeneralCollection
        {
            get { return generalCollection; }
            set { generalCollection = value; }
        }
               
        /// <summary>
        /// Текст для поиска по ФИО
        /// </summary>
        public string FilterByName
        {
            get { return (string)GetValue(FilterByNameProperty); }
            set { SetValue(FilterByNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterByName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterByNameProperty =
            DependencyProperty.Register("FilterByName", typeof(string), typeof(dbViewModel), new PropertyMetadata("", FilterByName_Changed));

        /// <summary>
        /// Текст для поиска по Отделу
        /// </summary>
         public string FilterByDepartment
        {
            get { return (string)GetValue(FilterByDepartmentProperty); }
            set { SetValue(FilterByDepartmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterByDepartment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterByDepartmentProperty =
            DependencyProperty.Register("FilterByDepartment", typeof(string), typeof(dbViewModel), new PropertyMetadata("", FilterByDepartment_Changed));




        public string FilterByStatus
        {
            get { return (string)GetValue(FilterByStatusProperty); }
            set { SetValue(FilterByStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterByStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterByStatusProperty =
            DependencyProperty.Register("FilterByStatus", typeof(string), typeof(dbViewModel), new PropertyMetadata("", FilterByStatus_Changed));




        public string FilterByPost
        {
            get { return (string)GetValue(FilterByPostProperty); }
            set { SetValue(FilterByPostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterByPost.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterByPostProperty =
            DependencyProperty.Register("FilterByPost", typeof(string), typeof(dbViewModel), new PropertyMetadata("", FilterByPost_Changed));

        #endregion

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

            //SetGeneralInfo();
            SetGeneralСollection();
            GeneralInfoCollection.Filter += Filter;
        }

        #region FIO_Filter
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Проверяет возможность фильтрации по ФИО
        /// </summary>      
        private bool FilterFIO(object obj)
        {
            bool result = true;
            GeneralPersonsInfo currentPerson = obj as GeneralPersonsInfo;
            if (!string.IsNullOrEmpty(FilterByName) &&  currentPerson != null && !currentPerson.Name.ToLower().Contains(FilterByName.ToLower()))
            {
                return false;
            }
            return result;
        }

        private bool Filter(object person)
        {
            GeneralPersonsInfo current = person as GeneralPersonsInfo;
            //you can write logic for filter here
            //if (!string.IsNullOrEmpty(FilterByName) && !string.IsNullOrEmpty(FilterByDepartment))
            //    return current.Department.Contains(FilterByDepartment) && current.Name.Contains(FilterByName);
            //else if (string.IsNullOrEmpty(FilterByName))
            //    return current.Department.Contains(FilterByDepartment);
            //else
            //    return current.Name.Contains(FilterByName);

            if (!string.IsNullOrEmpty(FilterByName) && !string.IsNullOrEmpty(FilterByDepartment) && !string.IsNullOrEmpty(FilterByStatus))
                return current.Department.Contains(FilterByDepartment) && current.Name.Contains(FilterByName) && current.Status.Contains(FilterByStatus);
            else if (string.IsNullOrEmpty(FilterByName))
                return current.Department.Contains(FilterByDepartment);
            else
                return current.Name.Contains(FilterByName);
        }

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Обработчик события изменения текста для поиска по ФИО
        /// </summary>       
        private static void FilterByName_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as dbViewModel;
            if (current != null)
            {
                //current.GeneralInfoCollection.Filter = null;
                //current.GeneralInfoCollection.Filter += current.FilterFIO;
                current.GeneralInfoCollection.Filter += current.Filter;
            }
        }

        private static void FilterByStatus_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as dbViewModel;
            if (current != null)
            {
                current.GeneralInfoCollection.Filter += current.Filter;
            }
        }

        private static void FilterByPost_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddFilter(d);
        }

        private static void AddFilter(DependencyObject d)
        {
            var current = d as dbViewModel;
            if (current != null)
            {
                current.GeneralInfoCollection.Filter += current.Filter;
            }
        }

        #endregion


        #region Department_Filter
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Проверяет возможность фильтрации по Отделу
        /// </summary>        
        private bool FilterDepartment(object obj)
        {
            bool result = true;
            GeneralPersonsInfo currentPerson = obj as GeneralPersonsInfo;
            if (!string.IsNullOrEmpty(FilterByDepartment) && currentPerson != null && !currentPerson.Department.ToLower().Contains(FilterByDepartment.ToLower()))
            {
                return false;
            }
            return result;
        }

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Обработчик события изменения текста для поиска по ФИО
        /// </summary>       
        private static void FilterByDepartment_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {           
            var current = d as dbViewModel;
            if (current != null)
            {
                //current.GeneralInfoCollection.Filter = null;
                //current.GeneralInfoCollection.Filter += current.FilterDepartment;
                current.GeneralInfoCollection.Filter += current.Filter;
            }
        }

        #endregion

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

            GeneralInfoCollection = CollectionViewSource.GetDefaultView(GeneralInfo);
        }

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Задает общий список, с информацией о сотрудниках
        /// </summary>
        private void SetGeneralСollection()
        {
            GeneralCollection = new ObservableCollection<GeneralPersonsInfo>();

            foreach (var person in Persons)
            {
                string io = person.FirstName.ElementAt(0) + "." + person.LastName.ElementAt(0) + ".";

                GeneralCollection.Add(new GeneralPersonsInfo()
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

            GeneralInfoCollection = CollectionViewSource.GetDefaultView(GeneralCollection);
        }
        //----------------------------------------------------------------------------------------------------

    }
    //----------------------------------------------------------------------------------------------------

}
