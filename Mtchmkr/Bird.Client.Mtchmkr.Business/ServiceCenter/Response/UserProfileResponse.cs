using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Business.ServiceCenter.Response
{
    public class UserProfileResponse
    {
        public Guid profileId { get; set; }
        public Guid userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string telephone { get; set; }
        public string imageTitle { get; set; }
        public string imageData { get; set; }
        public string place { get; set; }
        public int radiusLocator { get; set; }
        public string regularAvailability { get; set; }
        public string preferredgames { get; set; }
        public int rating { get; set; }
        public string playsAgainst { get; set; }
        public string notificationMethod { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class ProfileResuest
    {
        public Guid userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string telephone { get; set; }
        public string imageTitle { get; set; }
        public string imageData { get; set; }
        public string place { get; set; }
        public int radiusLocator { get; set; }
        public string regularAvailability { get; set; }
        public string preferredgames { get; set; }
        public int rating { get; set; }
        public string playsAgainst { get; set; }
        public string notificationMethod { get; set; }
    }

    public class PlayerDTO : INotifyPropertyChanged
    {
        public Guid userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string telephone { get; set; }
        public string place { get; set; }
        public int radiusLocator { get; set; }
        public string regularAvailability { get; set; }
        public string preferredgames { get; set; }
        public int rating { get; set; }
        public string playsAgainst { get; set; }
        public string notificationMethod { get; set; }
        public string imageTitle { get; set; }
        public string imageExtension { get; set; }
        public Guid gameId { get; set; }
        public string gameName { get; set; }
        public int maxFrame { get; set; }

        private string _imageData;
        public string imageData
        {
            get { return _imageData; }
            set
            {
                _imageData = value;
                OnPropertyChanged("imageData");

                if (!string.IsNullOrEmpty(_imageData))
                {
                    PlayerImage = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(_imageData)));
                }
                else
                {
                    PlayerImage = ImageSource.FromResource("PoolNew.jpg");
                }
            }
        }

        private ImageSource playerImage;
        public ImageSource PlayerImage
        {
            get { return playerImage; }
            set
            {
                playerImage = value;
                OnPropertyChanged("PlayerImage");
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class PlayerBooking
    {
        public Guid gameId { get; set; }
        public DateTime date { get; set; }
        public Guid locationId { get; set; }
        public Guid requestedToUser { get; set; }
        public Guid createdByUser { get; set; }
        public DateTime createdDate { get; set; }
        public bool isAgreed { get; set; }
        public Guid matchId { get; set; }
    }
}

