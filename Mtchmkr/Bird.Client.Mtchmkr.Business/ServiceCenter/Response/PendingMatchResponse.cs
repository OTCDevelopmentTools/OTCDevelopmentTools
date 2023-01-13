using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Business.ServiceCenter.Response
{
    public class PendingMatchResponse : INotifyPropertyChanged
    {
        public Guid userID { get; set; }
        public string requestedToUser { get; set; }
        public string name { get; set; }
        public string gameName { get; set; }
        public string createdByUser { get; set; }
        public Guid matchId { get; set; }
        public DateTime matchDate { get; set; }
        public string location { get; set; }

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
