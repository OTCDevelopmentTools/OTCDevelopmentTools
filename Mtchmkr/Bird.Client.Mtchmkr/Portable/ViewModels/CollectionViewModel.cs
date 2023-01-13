using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class CollectionViewModel<T> : BaseViewModel where T: INotifyPropertyChanged
    {
        private ObservableCollection<T> m_collection;
        public event PropertyChangedEventHandler ItemPropertyChanged;
        public CollectionViewModel()
        {
        }
        public CollectionViewModel(INavigation navigation) : base(navigation)
        {
        }
        protected virtual void LoadData()
        {

        }
        public ObservableCollection<T> Collection
        {
            get
            {
                if (null == m_collection)
                {
                    m_collection = new ObservableCollection<T>();
                    m_collection.CollectionChanged += M_collection_CollectionChanged;
                    LoadData();
                }
                return m_collection;
            }
            set
            {
                if (m_collection == value) return;
                RemoveAll();
                m_collection = value;
                AddAll();
                OnPropertyChanged(nameof(Collection));
            }
        }
        void RemoveAll()
        {
            foreach (INotifyPropertyChanged item in m_collection)
            {
                item.PropertyChanged -= Item_PropertyChanged;
            }
            m_collection.CollectionChanged -= M_collection_CollectionChanged;
        }
        void AddAll()
        {
            foreach (INotifyPropertyChanged item in m_collection)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
            m_collection.CollectionChanged += M_collection_CollectionChanged;
        }
        private void M_collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    //Added items
                    //      item.PropertyChanged += EntityViewModelPropertyChanged;
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }

        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            ItemPropertyChanged?.Invoke(sender , e);
        }
        public int Count
        {
            get => Collection.Count;
        }

        public override void Dispose()
        {
            base.Dispose();
            if (m_collection == null) return;
            RemoveAll();
        }
    }
}
