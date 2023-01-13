using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Business.ServiceCenter.Response
{
    public class ShowCaseResponse : INotifyPropertyChanged
    {
        public Guid userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string telephone { get; set; }
        public string place { get; set; }
        public int? radiusLocator { get; set; }
        public string regularAvailability { get; set; }
        public string preferredgames { get; set; }
        public int? rating { get; set; }
        public string playsAgainst { get; set; }
        public string notificationMethod { get; set; }
        public string imageTitle { get; set; }
        public string imageExtension { get; set; }
        public Guid matchId { get; set; }
        public string frames { get; set; }
        public DateTime date { get; set; }
        public string time { get; set; }
        public string location { get; set; }

        private string _gameName;
        public string gameName
        {
            get { return _gameName; }
            set
            {
                _gameName = value;
                OnPropertyChanged("gameName");

                if (!string.IsNullOrEmpty(gameName))
                {
                    if (gameName.Equals("Snooker"))
                    {
                        GameImage = "Snooker2.jpg";
                    }
                    else if (gameName.Equals("Pool"))
                    {
                        GameImage = "PoolNew.jpg";
                    }
                    else if (gameName.Equals("9 Ball"))
                    {
                        GameImage = "NineBall.jpg";
                    }
                    else
                    {
                        GameImage = "NineBall.jpg";
                    }

                }
            }
        }

        private string gameImage;
        public string GameImage
        {
            get { return gameImage; }
            set
            {
                gameImage = value;
                OnPropertyChanged("GameImage");
            }
        }

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
}
