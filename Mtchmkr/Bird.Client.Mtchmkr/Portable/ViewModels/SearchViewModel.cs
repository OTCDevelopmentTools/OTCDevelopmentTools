using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.XamarinForms.Primitives.CheckBox.Commands;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    internal class SearchViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;
        private List<ListItem> m_Skills;

        private ObservableCollection<GamesModel> gamesItemSource;
        public ObservableCollection<GamesModel> GamesItemSource
        {
            get
            {
                return gamesItemSource;
            }
            set
            {
                gamesItemSource = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PlayerDTO> playersItemsSource;
        public ObservableCollection<PlayerDTO> PlayersItemsSource
        {
            get
            {
                return playersItemsSource;
            }
            set
            {
                playersItemsSource = value;
                OnPropertyChanged();
            }
        }

        private GamesModel selectedGame;
        public GamesModel SelectedGame
        {
            get
            {
                return selectedGame;
            }
            set
            {
                selectedGame = value;
                OnPropertyChanged();
            }
        }

        PlayerDTO _player;

        private ObservableCollection<LocationModel> locationItemsSource;
        public ObservableCollection<LocationModel> LocationItemsSource
        {
            get
            {
                return locationItemsSource;
            }
            set
            {
                locationItemsSource = value;
                OnPropertyChanged();
            }
        }

        private LocationModel selectedLocation;
        public LocationModel SelectedLocation
        {
            get
            {
                return selectedLocation;
            }
            set
            {
                selectedLocation = value;
                OnPropertyChanged();
            }
        }

        private DateTime? bookingDate = null;
        public DateTime? BookingDate
        {
            get
            {
                return bookingDate;
            }
            set
            {
                bookingDate = value;
                OnPropertyChanged();
            }
        }

        public ICommand Accept { get; set; }
        public ICommand Cancel { get; set; }

        public SearchViewModel(INavigation navigation) : base(navigation)
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
            Load();

            GetGamesMethod();

            LocationItemsSource = new ObservableCollection<LocationModel>();
            //_player = player;

            

            this.Accept = new Command(this.OnAccept);
            this.Cancel = new Command(this.OnCancel);
        }
       
        void Load()
        { 
            m_Skills = new List<ListItem>();
            m_Skills.Add(new ListItem { Value = 1, Text = "1 Star" });
            m_Skills.Add(new ListItem { Value = 2, Text = "2 Star" });
            m_Skills.Add(new ListItem { Value = 3, Text = "3 Star" });
            m_Skills.Add(new ListItem { Value = 4, Text = "4 Star" });
            m_Skills.Add(new ListItem { Value = 5, Text = "5 Star" });
        }
        public List<ListItem> Skills
        {
            get { return m_Skills; }
        }
        
        private int m_Distance;
        public int Distance
        {
            get { return m_Distance; }
            set
            {
                if (m_Distance == value) return;
                m_Distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }
        private int m_Minimum=2;
        public int Minimum
        {
            get { return m_Minimum; }
            set
            {
                if (m_Minimum == value) return;
                if (m_Maximum < value)
                {
                    Maximum = value;
                }

                m_Minimum = value;
                OnPropertyChanged(nameof(Minimum));

            }
        }
        private int m_Maximum=5;
        public int Maximum
        {
            get { return m_Maximum; }
            set
            {
                if (m_Maximum == value) return;
                if (m_Minimum > value)
                {
                    Minimum =value;
                }
                m_Maximum = value;
                OnPropertyChanged(nameof(Maximum));
            }
        }
        private bool m_CashGame=false;
        public bool CashGame
        {
            get { return m_CashGame; }
            set
            {
                if (m_CashGame == value) return;
                m_CashGame = value;
                OnPropertyChanged(nameof(IsFriendly));
                OnPropertyChanged(nameof(IsCash));
            }
        }

        public bool IsFriendly
        {
            get { return !CashGame; }
            set
            {
                CashGame = !value;
            }
        }

        public bool IsCash
        {
            get { return CashGame; }
            set
            {
                CashGame=value;
            }
        }
        

        private string frame;
        public string Frame
        {
            get
            {
                return frame;
            }
            set
            {
                frame = value;
                OnPropertyChanged();
            }
        }


        public ICommand SearchCommand { get => new Command(async () => await Search()); }

        async Task Search()
        {
            SearchPlayersMethod();
        }


        public async void GetGamesMethod()
        {
            _progDialog.ShowProgress("Loading...");
            var result = await App.ServiceManager.GetGamesAsync();
            if (result != null)
            {
                GamesItemSource = new ObservableCollection<GamesModel>(result);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "No Data Found", "Ok");
            }

            _progDialog.HideProgress();

            GetLocationsMethod();
        }

        public async void SearchPlayersMethod()
        {
            if (SelectedGame != null && SelectedLocation != null)
            {
                if (BookingDate == null)
                {
                    await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Please select date for match.", "Ok");
                    return;
                }

                _progDialog.ShowProgress("Loading...");
                var userId = Guid.Parse(Preferences.Get("UserId", string.Empty));
                var result = await App.ServiceManager.GetSearchPlayersAsync(SelectedGame.gameId, Minimum, Maximum, Distance, Frame, userId, Convert.ToDateTime(BookingDate));
                if (result != null && result.Count > 0)
                {
                    PlayersItemsSource = new ObservableCollection<PlayerDTO>(result);
                    await Navigation.PushAsync(new SearchResultsPage(PlayersItemsSource,SelectedLocation.locationId, Convert.ToDateTime(BookingDate)));
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "No Data Found", "Ok");
                }

                _progDialog.HideProgress();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Please select game and location", "Ok");
            }
        }

        public async void GetLocationsMethod()
        {
            _progDialog.ShowProgress("Loading...");
            var result = await App.ServiceManager.GetLocationsAsync();
            if (result != null)
            {
                LocationItemsSource = new ObservableCollection<LocationModel>(result);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Locations not found", "Ok");
            }

            _progDialog.HideProgress();
        }

        private void OnAccept(object param)
        {
            //Application.Current.MainPage.DisplayAlert("Date selected", String.Format("New Date: {0:g}", (DateTime)param), "OK");
            // implement your custom logic here

            BookingDate = (DateTime)param;
        }

        private void OnCancel(object param)
        {
            BookingDate = null;
        }
    }
}