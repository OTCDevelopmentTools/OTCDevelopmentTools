using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Request;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Plugin.Media;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class EditProfilePageViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;
        private string profileImageBase64Path = string.Empty;
        private string profileImageExtension = string.Empty;
        ImageDTO requestImage;
        UserProfileResponse _profileData;

        private string regularAvailability = string.Empty;
        public string RegularAvailability
        {
            get => regularAvailability;
            set
            {
                if (regularAvailability == value) return;
                regularAvailability = value;
                OnPropertyChanged(nameof(RegularAvailability));
            }
        }

        private string place = string.Empty;
        public string Place
        {
            get => place;
            set
            {
                if (place == value) return;
                place = value;
                OnPropertyChanged(nameof(Place));
            }
        }

        private int radius;
        public int Radius
        {
            get => radius;
            set
            {
                if (radius == value) return;
                radius = value;
                OnPropertyChanged(nameof(Radius));
            }
        }
        
        private ImageSource profilePhoto;
        public ImageSource ProfilePhoto
        {
            get
            {
                return profilePhoto;
            }
            set
            {
                profilePhoto = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GamesModel> gameSource;
        public ObservableCollection<GamesModel> GameSource
        {
            get
            {
                return gameSource;
            }
            set
            {
                gameSource = value;
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

        private string notificationMethod = "Email";
        public string NotificationMethod
        {
            get
            {
                return notificationMethod;
            }
            set
            {
                notificationMethod = value;
                OnPropertyChanged();
            }
        }

        private string playsAgainst = string.Empty;
        public string PlaysAgainst
        {
            get
            {
                return playsAgainst;
            }
            set
            {
                playsAgainst = value;
                OnPropertyChanged();
            }
        }

        private Command editPhotoCommand;
        public Command EditPhotoCommand
        {
            get
            {
                return editPhotoCommand ?? new Command(async (obj) =>
                {
                    try
                    {
                        var _photoMode = await App.Current.MainPage.DisplayActionSheet(Constants.APP_NAME, "Cancel", null, new string[] { "Gallery" });

                        //if (_photoMode.Equals("Camera"))
                        //{
                        //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                        //    {
                        //        await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                        //        return;
                        //    }
                        //    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                        //    {
                        //        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                        //        Directory = "Sample",
                        //        Name = "test.jpg"
                        //    });
                        //    if (file == null)
                        //        return;

                        //    var photoPath = file.Path;

                        //    Byte[] bytes = File.ReadAllBytes(photoPath);

                        //    profileImageBase64Path = Convert.ToBase64String(bytes);

                        //    FileInfo fi = new FileInfo(file.Path);
                        //    profileImageExtension = fi.Extension;

                        //    ProfilePhoto = ImageSource.FromStream(() =>
                        //    {
                        //        var stream = file.GetStream();
                        //        file.Dispose();
                        //        return stream;
                        //    });
                        //}


                        if (!CrossMedia.Current.IsPickPhotoSupported)
                        {
                            await App.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                            return;
                        }
                        var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                        {
                            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                        });

                        if (file == null)
                            return;

                        var photoPath = file.Path;

                        Byte[] bytes = File.ReadAllBytes(photoPath);

                        profileImageBase64Path = Convert.ToBase64String(bytes);

                        FileInfo fi = new FileInfo(file.Path);
                        profileImageExtension = fi.Extension;

                        ProfilePhoto = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            file.Dispose();
                            return stream;
                        });

                        requestImage = new ImageDTO();
                        requestImage.userId = Guid.Parse(Preferences.Get("UserId", string.Empty));
                        requestImage.imageData = profileImageBase64Path;
                        requestImage.imageExtension = profileImageExtension;
                        requestImage.imageTitle = fi.Name;
                        requestImage.createdDate = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ProfileViewModel : " + ex.Message);
                    }
                });
            }
        }

        public Command UpdateCommand { get => new Command(() => UpdateCommandMethod()); }

        public EditProfilePageViewModel(UserProfileResponse profileData)
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
            _profileData = profileData;
            RegularAvailability = _profileData.regularAvailability;
            Place = _profileData.place;
            Radius = _profileData.radiusLocator;
          //  SelectedGame = _profileData.preferredgames;
            profileImageBase64Path = _profileData.imageData;
            profileImageExtension = _profileData.imageTitle;
            
            PlaysAgainst = _profileData.playsAgainst;

            if (string.IsNullOrEmpty(_profileData.notificationMethod))
            {
                NotificationMethod = "Email";
            }
            else
            {
                NotificationMethod = _profileData.notificationMethod;
            }

            if (string.IsNullOrEmpty(_profileData.playsAgainst))
            {
                PlaysAgainst = "";
            }
            else
            {
                PlaysAgainst = _profileData.playsAgainst;
            }
            GetGamesMethod();
        }

        public async void GetGamesMethod()
        {
            _progDialog.ShowProgress("Loading...");
            var result = await App.ServiceManager.GetGamesAsync();
            if (result != null)
            {
                GameSource = new ObservableCollection<GamesModel>(result);
                SelectedGame = GameSource.FirstOrDefault(x => x.name == _profileData.preferredgames);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "No Data Found", "Ok");
            }

            _progDialog.HideProgress();
        }

        private async void UpdateCommandMethod()
        {

            _progDialog.ShowProgress("Loading...");

            if (requestImage != null)
            {
                var uploadImageResult = await UploadImage(requestImage);
            }

            ProfileResuest request = new ProfileResuest
            {
                userId = Guid.Parse(Preferences.Get("UserId", string.Empty)),
                email = _profileData.email,
                imageData = profileImageBase64Path,
                imageTitle = profileImageExtension,
                name = _profileData.name,
                notificationMethod = NotificationMethod,
                place = Place,
                playsAgainst = PlaysAgainst,
                preferredgames = SelectedGame.name,
                radiusLocator = Radius,
                rating = _profileData.rating,
                regularAvailability = RegularAvailability,
                telephone = _profileData.telephone,
                userName = _profileData.userName,
                gameId=SelectedGame.gameId.ToString()
            };

            var result = await App.ServiceManager.UpdateUserProfileAsync(request);

            if (result)
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Somthing went wrong, please try again", "Ok");
            }

            

            _progDialog.HideProgress();
        }

        public async Task<bool> UploadImage(ImageDTO _request)
        {
            var result = await App.ServiceManager.UploadImageAsync(_request);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}