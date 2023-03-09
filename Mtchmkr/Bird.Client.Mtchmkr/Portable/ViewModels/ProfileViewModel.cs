using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Request;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.Views;
using Plugin.Media;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;

        

        private Command editProfileCommand;
        public Command EditProfileCommand
        {
            get
            {
                return editProfileCommand ?? new Command(async (obj) =>
                {
                    await App.Current.MainPage.Navigation.PushAsync(new EditProfilePage(ProfileData));
                });
            }
        }

        public ProfileViewModel()
        {
            Title = "Profile";
            _progDialog = DependencyService.Get<IProgressDialog>();
        }

        //public ProfileViewModel(BasePlayerModel profile):this()
        //{
        //    Profile = profile;
        //    _progDialog = DependencyService.Get<IProgressDialog>();

        //    GetProfile();
        //}

        public ICommand LogOutCommand { get => new Command(async () => await Logout()); }

        async Task Logout()
        {
            var result = await App.LogOut();
        }

        //private BasePlayerModel m_Profile;
        //public BasePlayerModel Profile
        //{
        //    get { return m_Profile; }
        //    set
        //    {
        //        if (m_Profile == value) return;
        //        m_Profile = value;
        //        OnPropertyChanged(nameof(Profile));
        //    }
        //}

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string telephone;
        public string Telephone
        {
            get { return telephone; }
            set
            {
                telephone = value;
                OnPropertyChanged("Telephone");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string regularAvailability;
        public string RegularAvailability
        {
            get { return regularAvailability; }
            set
            {
                regularAvailability = value;
                OnPropertyChanged("RegularAvailability");
            }
        }

        private string preferredgames;
        public string Preferredgames
        {
            get { return preferredgames; }
            set
            {
                preferredgames = value;
                OnPropertyChanged("Preferredgames");
            }
        }

        private bool _isRatingChanging;
        private int rating;
        public int Rating
        {
            get { return rating; }
            set
            {
                rating = value;
                OnPropertyChanged("Rating");
                UpdateCommandMethod();
            }
        }

        private string playsAgainst;
        public string PlaysAgainst
        {
            get { return playsAgainst; }
            set
            {
                playsAgainst = value;
                OnPropertyChanged("PlaysAgainst");
            }
        }

        private string place;
        public string Place
        {
            get { return place; }
            set
            {
                place = value;
                OnPropertyChanged("Place");
            }
        }

        private string radiusLocator;
        public string RadiusLocator
        {
            get { return radiusLocator; }
            set
            {
                radiusLocator = value;
                OnPropertyChanged("RadiusLocator");
            }
        }

        private string notificationMethod;
        public string NotificationMethod
        {
            get { return notificationMethod; }
            set
            {
                notificationMethod = value;
                OnPropertyChanged("NotificationMethod");
            }
        }

        private string imageBase64;
        public string ImageBase64
        {
            get { return imageBase64; }
            set
            {
                imageBase64 = value;
                OnPropertyChanged("ImageBase64");

                if (!string.IsNullOrEmpty(ImageBase64))
                {
                    Image = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(imageBase64)));
                }
                else
                {
                    Image = ImageSource.FromResource("placeholder.png");
                }
            }
        }

        private Xamarin.Forms.ImageSource image;
        public Xamarin.Forms.ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        private UserProfileResponse profileData;

        public UserProfileResponse ProfileData
        {
            get { return profileData; }
            set
            {
                profileData = value;
                OnPropertyChanged("ProfileData");
            }
        }

        private async void UpdateCommandMethod()
        {
            if (_isRatingChanging)
                return;
            _progDialog.ShowProgress("Loading...");

            ProfileResuest request = new ProfileResuest
            {
                userId = Guid.Parse(Preferences.Get("UserId", string.Empty)),
                email = ProfileData.email,
                imageData = ProfileData.imageData,
                imageTitle = ProfileData.imageTitle,
                name = ProfileData.name,
                notificationMethod = ProfileData.notificationMethod,
                place = ProfileData.place,
                playsAgainst = ProfileData.playsAgainst,
                preferredgames = ProfileData.preferredgames,
                radiusLocator = ProfileData.radiusLocator,
                rating = Rating,
                regularAvailability = ProfileData.regularAvailability,
                telephone = ProfileData.telephone,
                userName = ProfileData.userName
            };

            var result = await App.ServiceManager.UpdateUserProfileAsync(request);

            if (!result)
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Somthing went wrong, please try again", "Ok");
            }

            _progDialog.HideProgress();
        }

        public async void GetProfile()
        {
            _progDialog.ShowProgress("Loading...");
            var userId = Guid.Parse(Preferences.Get("UserId", string.Empty));
            var result = await App.ServiceManager.GetProfileByIdAsync(userId);
            if (result != null)
            {
                ProfileData = result;
                Name = result.name;
                ImageBase64 = result.imageData;
                Email = result.email;
                Telephone = result.telephone;
                RegularAvailability = result.regularAvailability;
                Place = result.place;
                PlaysAgainst = result.playsAgainst;
                Preferredgames = result.preferredgames;
                _isRatingChanging = true;
                Rating = result.rating;
                await Task.Delay(10);
                _isRatingChanging = false;
                RadiusLocator = result.radiusLocator.ToString() + " Miles";
                NotificationMethod = result.notificationMethod;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Somthing went wrong, please try again", "Ok");
            }

            _progDialog.HideProgress();
        }

    }
}