using Bird.Client.Mtchmkr.Portable.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class GameTypesViewModel : CollectionViewModel<GameTypeModel>
    {
        private bool m_IsEmpty = false;
//        private ObservableCollection<GameTypeModel> m_Games;
        //public ObservableCollection<GameTypeModel> Games
        //{
        //    get
        //    {
        //        if (null == m_Games)
        //        {
        //            m_Games = new ObservableCollection<GameTypeModel>();
        //            m_Games.CollectionChanged += M_Games_CollectionChanged;
        //            foreach(var item in CreateGames())
        //                m_Games.Add(item);
        //        }
        //        return m_Games;
        //    }
        //    set { m_Games = value; }
        //}
        public GameTypesViewModel()
        {
           
            this.ItemPropertyChanged += GameTypesViewModel_ItemPropertyChanged;
            try
            {
                foreach (var item in CreateGames())
                {
                    Collection.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GameTypesViewModel_ItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SelectedUpdate();
        }

        //private void M_Games_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.Action == NotifyCollectionChangedAction.Remove)
        //    {
        //        foreach (GameTypeModel item in e.OldItems)
        //        {
        //            //Removed items
        //            item.PropertyChanged -= Item_PropertyChanged;
        //        }
        //    }
        //    else if (e.Action == NotifyCollectionChangedAction.Add)
        //    {
        //        foreach (GameTypeModel item in e.NewItems)
        //        {
        //            //Added items
        //            //      item.PropertyChanged += EntityViewModelPropertyChanged;
        //            item.PropertyChanged += Item_PropertyChanged;
        //        }
        //    }
        //}

        //private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    SelectedUpdate();
        //    this.OnPropertyChanged(e.PropertyName);
        //}
        void SelectedUpdate()
        {
            IsEmpty = (Selected == 0);
        }
        public bool IsEmpty
        {
            get { return m_IsEmpty; }
            set 
            {
                if (m_IsEmpty == value) return;
                m_IsEmpty = value; 
                OnPropertyChanged(nameof(IsEmpty));
            }
        }
      
        public ICommand Update
        {
            get =>  new Command(UpdateValues);
        }

         void UpdateValues()
        {
             Console.WriteLine("Games Count : {0}", Selected);
        }
        //public int Count
        //{
        //    get => Games.Count;
        //}
        public int Selected
        {
            get
            {
                int counter = 0;
                foreach(GameTypeModel gameType in Collection)
                {
                    if (gameType.Selected) counter++;
                }
                return counter;
            }
        }
        private GameTypeModel[] CreateGames()
        {
            return new GameTypeModel[]
            {
                new GameTypeModel { Key = System.Guid.NewGuid(), Name = "Snooker", Selected=true, Description="Description", Colour = System.Drawing.Color.Red , Image="Snooker.jpg"},
                new GameTypeModel { Key = System.Guid.NewGuid(), Name = "Pool", Description="Description", Colour = System.Drawing.Color.Blue,  Image="Pool.jpg" } ,
                new GameTypeModel { Key = System.Guid.NewGuid(), Name = "Bar Billiards", Description="Bar billiards is a form of billiards which involves scoring points by potting balls in holes on the playing surface of the table rather than in pockets. Bar billiards developed from the French/Belgian game billiard russe,", Colour = System.Drawing.Color.Green,  Image="BarBilliards.jpg" }

            };
        }
    }
}