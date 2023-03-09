using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class CreateBookingPageViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;
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

        private DateTime bookingDate;
        public DateTime BookingDate
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

        public CreateBookingPageViewModel(PlayerDTO player)
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
            LocationItemsSource = new ObservableCollection<LocationModel>();
            _player = player;

            GetLocationsMethod();

            this.Accept = new Command(this.OnAccept);
            this.Cancel = new Command(this.OnCancel);
        }

        public ICommand BookCommand { get => new Command(() => CreateBooking()); }

        async Task CreateBooking()
        {
            if (SelectedLocation != null && BookingDate != null)
            {
                _progDialog.ShowProgress("Loading...");
                PlayerBooking obj = new PlayerBooking();
                //obj.createdByUser = Guid.Parse(Preferences.Get("UserId", string.Empty));
                //obj.gameId = _player.gameId;
                //obj.locationId = SelectedLocation.locationId;
                //obj.requestedToUser = _player.userId;
                //obj.date = BookingDate;
                //obj.createdDate = DateTime.Now;
                //obj.isAgreed = false;

                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);


                var Result = await PlayerBookingMethod(obj);
                if (Result)
                {
                    await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "MTCH request sent.", "Ok");
                    App.Current.MainPage.Navigation.PopToRootAsync(true);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "MTCH request failed for MTCH.", "Ok");
                }

                _progDialog.HideProgress();
            }
            else {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Please select Date and Location", "Ok");
            }
        }

        public async Task<bool> PlayerBookingMethod(PlayerBooking _request)
        {
            //var result = await App.ServiceManager.PlayerBookingAsync(_request);
            //if (result)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return false;
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
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "No Data Found", "Ok");
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
            var message = param != null ? String.Format("Current date: {0:g}", (DateTime)param) : "Currently no date is selected";
            Application.Current.MainPage.DisplayAlert("Date Selection Canceled", message, "OK");
            // implement your custom logic here
        }
    }
}