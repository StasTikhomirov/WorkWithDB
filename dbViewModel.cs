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

        /// <summary>
        /// Коллекция данных для отображения
        /// </summary>
        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(dbViewModel), new PropertyMetadata(null));

        /// <summary>
        /// Задает коллекцию для работы с данными
        /// </summary>
        public dbViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(Person.GetPersons());
        }
    }
}
