using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace WorkWithDB
{
    class dbViewModel : DependencyObject, INotifyPropertyChanged
    {
        private ICollectionView generalInfoCollection;
        private ObservableCollection<GeneralPersonsInfo> generalCollection;

        private bool employ;
        private bool unEmploy;
        private DateTime? dateFrom;
        private DateTime? dateTo;
        private string selectedStatus;
        private string outputText;

        #region Properties

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Cписок сотрудников
        /// </summary>
        public List<Person> Persons
        {
            get { return (List<Person>)GetValue(PersonsProperty); }
            set { SetValue(PersonsProperty, value); }
        }

        public static readonly DependencyProperty PersonsProperty =
            DependencyProperty.Register("Items", typeof(List<Person>), typeof(dbViewModel), new PropertyMetadata(null));

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Cписок должностей
        /// </summary>
        public List<Post> Posts
        {
            get { return (List<Post>)GetValue(PostsProperty); }
            set { SetValue(PostsProperty, value); }
        }

        public static readonly DependencyProperty PostsProperty =
            DependencyProperty.Register("Posts", typeof(List<Post>), typeof(dbViewModel), new PropertyMetadata(null));

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Список статусов
        /// </summary>
        public List<Status> Statuses
        {
            get { return (List<Status>)GetValue(StatusesProperty); }
            set { SetValue(StatusesProperty, value); }
        }

        public static readonly DependencyProperty StatusesProperty =
            DependencyProperty.Register("Statuses", typeof(List<Status>), typeof(dbViewModel), new PropertyMetadata(null));

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Cписок отделов
        /// </summary>
        public List<Department> Departments
        {
            get { return (List<Department>)GetValue(DepartmentsProperty); }
            set { SetValue(DepartmentsProperty, value); }
        }

        public static readonly DependencyProperty DepartmentsProperty =
            DependencyProperty.Register("Departments", typeof(List<Department>), typeof(dbViewModel), new PropertyMetadata(null));

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Общий список данных о сотрудниках
        /// </summary>
        //public List<GeneralPersonsInfo> GeneralInfo
        //{
        //    get { return (List<GeneralPersonsInfo>)GetValue(GeneralInfoProperty); }
        //    set { SetValue(GeneralInfoProperty, value); }
        //}

        //public static readonly DependencyProperty GeneralInfoProperty =
        //    DependencyProperty.Register("GeneralInfo", typeof(List<GeneralPersonsInfo>), typeof(dbViewModel), new PropertyMetadata(null));

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Коллекция для отображения 
        /// </summary>
        public ICollectionView GeneralInfoCollection
        {
            get { return generalInfoCollection; }
            set { generalInfoCollection = value; }               
        }

        /// <summary>
        /// Общая коллекция с данными о сотрудниках
        /// </summary>
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


        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Принят на работу
        /// </summary>
        public bool Employ
        {
            get
            {
                return employ;
            }
            set
            {
                employ = value;
                OnPropertyChanged(nameof(Employ));
            }
        }

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Уволен с работы
        /// </summary>
        public bool UnEmploy
        {
            get
            {
                return unEmploy;
            }
            set
            {
                unEmploy = value;
                OnPropertyChanged(nameof(UnEmploy));
            }
        }

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Teкст для вывода статистики
        /// </summary>
        public string OutputText
        {
            get
            {
                return outputText;
            }
            set
            {
                outputText = value;
                OnPropertyChanged(nameof(OutputText));
            }
        }

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Дата начала периода для поиска
        /// </summary>
        public DateTime? DateFrom
        {
            get
            {
                return dateFrom;
            }
            set
            {
                dateFrom = value;
                OnPropertyChanged(nameof(DateFrom));
            }
        }

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Дата начала периода для поиска
        /// </summary>
        public DateTime? DateTo
        {
            get
            {
                return dateTo;
            }
            set
            {
                dateTo = value;
                OnPropertyChanged(nameof(DateTo));
            }
        }

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Выбранный статус для отображения статистики
        /// </summary>
        public string SelectedStatus
        {
            get
            {
                return selectedStatus;
            }
            set
            {
                selectedStatus = value;
                OnPropertyChanged(nameof(SelectedStatus));
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Инициализирует ViewModel
        /// </summary>
        public dbViewModel()
        {
            // Получение данных из Моделей
            Persons = Person.GetPersons();
            Posts = Post.GetPosts();
            Statuses = Status.GetStatuses();
            Departments = Department.GetDepartments();

            // Заполнение коллекции для отображения            
            SetGeneralСollection();

            //Добавление фильтра к таблице
            //GeneralInfoCollection.Filter += Filter;

            // Изначальные критерии для статистики
            Employ = false;
            UnEmploy = false;
            DateFrom = Persons.Min(p => p.DateEmploy); // минимальная дата приема на работу
            DateTo = DateTime.Today;  
            
            // инициализация команды для выполнения поиска
            GetStatisticsCommand = new RelayCommand(GetStatictics);

            // 
            ClearFilterCommand = new RelayCommand(ClearFilter);
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

        //private bool Filter(object person)
        //{
        //    GeneralPersonsInfo current = person as GeneralPersonsInfo;
        //    //you can write logic for filter here
        //    //if (!string.IsNullOrEmpty(FilterByName) && !string.IsNullOrEmpty(FilterByDepartment))
        //    //    return current.Department.Contains(FilterByDepartment) && current.Name.Contains(FilterByName);
        //    //else if (string.IsNullOrEmpty(FilterByName))
        //    //    return current.Department.Contains(FilterByDepartment);
        //    //else
        //    //    return current.Name.Contains(FilterByName);

        //    if (!string.IsNullOrEmpty(FilterByName) && !string.IsNullOrEmpty(FilterByDepartment) && !string.IsNullOrEmpty(FilterByStatus))
        //        return current.Department.Contains(FilterByDepartment) && current.Name.Contains(FilterByName) && current.Status.Contains(FilterByStatus);
        //    else if (string.IsNullOrEmpty(FilterByName))
        //        return current.Department.Contains(FilterByDepartment);
        //    else
        //        return current.Name.Contains(FilterByName);
        //}

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
                // current.GeneralInfoCollection.Filter += current.Filter;
                current.GeneralInfoCollection.Filter = null;
                current.GeneralInfoCollection.Filter = current.FilterFIO;

            }
        }

        private static void FilterByStatus_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as dbViewModel;
            if (current != null)
            {
                //current.GeneralInfoCollection.Filter += current.Filter;
                current.GeneralInfoCollection.Filter = null;
                current.GeneralInfoCollection.Filter = current.FilterStatus;
            }
        }

        private static void FilterByPost_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //AddFilter(d);
            var current = d as dbViewModel;
            if (current != null)
            {
                //current.GeneralInfoCollection.Filter += current.Filter;
                current.GeneralInfoCollection.Filter = null;
                current.GeneralInfoCollection.Filter = current.FilterPost;
            }
        }

        //private static void AddFilter(DependencyObject d)
        //{
        //    var current = d as dbViewModel;
        //    if (current != null)
        //    {
        //        current.GeneralInfoCollection.Filter += current.Filter;
        //    }
        //}

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


        private bool FilterStatus(object obj)
        {
            bool result = true;
            GeneralPersonsInfo currentPerson = obj as GeneralPersonsInfo;
            if (!string.IsNullOrEmpty(FilterByStatus) && currentPerson != null && !currentPerson.Status.ToLower().Contains(FilterByStatus.ToLower()))
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
                current.GeneralInfoCollection.Filter = null;
                current.GeneralInfoCollection.Filter = current.FilterDepartment;
                //current.GeneralInfoCollection.Filter += current.Filter;
            }
        }

        #endregion


        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Проверяет возможность фильтрации по ФИО
        /// </summary>      
        private bool FilterPost(object obj)
        {
            bool result = true;
            GeneralPersonsInfo currentPerson = obj as GeneralPersonsInfo;
            if (!string.IsNullOrEmpty(FilterByPost) && currentPerson != null && !currentPerson.Post.ToLower().Contains(FilterByPost.ToLower()))
            {
                return false;
            }
            return result;
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
       /// <summary>
       /// Команда отображения статистики
       /// </summary>
        public ICommand GetStatisticsCommand { get; set; }        
        private void GetStatictics(object sender)
        {
            int personsCount = 0;
            if (Employ)
            {
                personsCount = GeneralCollection.Count(i => i.Status == SelectedStatus && i.DateEmploy > DateFrom && (!i.DateUnEmploy.HasValue || i.DateEmploy < DateTo));
            }
            else
            {
                if (UnEmploy)
                {
                    personsCount = GeneralCollection.Count(i => i.Status == SelectedStatus && (!i.DateUnEmploy.HasValue || i.DateUnEmploy > DateFrom) && (!i.DateUnEmploy.HasValue || i.DateUnEmploy < DateTo));
                }
                else
                {
                    MessageBoxResult result = MessageBoxResult.No;

                    if (!string.IsNullOrEmpty(SelectedStatus))
                    {
                        result = MessageBox.Show("Вы не выбрали, кто вас интересует: Нанятые или Уволенные сотрудники. \n Показать количество всех сотрудников компании c этим статусом?", "Отсутствие выбора критерия", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            personsCount = GeneralCollection.Count(i => i.Status == SelectedStatus);
                        }
                    }
                    else
                    {
                        result = MessageBox.Show("Вы не выбрали ни одного критерия. \n Показать количество всех сотрудников компании?", "Отсутствие выбора критерия", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            personsCount = GeneralCollection.Count();
                        }
                    }       
                }
            }
            
            OutputText = "Количество сотрудников, удовлетворяющих условиям : " + personsCount.ToString();                      
        }

        public ICommand ClearFilterCommand { get; set; }

        private void ClearFilter(object sender)
        {
            var current = sender as dbViewModel;
            if (current != null)
            {
                current.GeneralInfoCollection.Filter = null;
            }
            FilterByName = string.Empty;
            FilterByDepartment = string.Empty;
            FilterByPost = string.Empty;
            FilterByStatus = string.Empty;
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

        //----------------------------------------------------------------------------------------------------

    }
    //----------------------------------------------------------------------------------------------------

}
