using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class BasePlayerModel : BaseModel , IPlayer
    {

        public Guid PlayerKey { get; set; }

        private string m_FirstName;
        public string FirstName
        {
            get { return m_FirstName; }
            set
            {
                if (m_FirstName == value) return;
                m_FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string m_LastName;
        public string LastName
        {
            get { return m_LastName; }
            set
            {
                if (m_LastName == value) return;
                m_LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        private string m_PlayerImage;
        public string PlayerImage
        {
            get { return m_PlayerImage; }
            set
            {
                if (m_PlayerImage == value) return;
                m_PlayerImage = value;
                OnPropertyChanged(nameof(PlayerImage));
            }
        }
        private int m_Skill;

        public int Skill
        {
            get { return m_Skill; }
            set
            {
                if (m_Skill == value) return;
                m_Skill = value;
                OnPropertyChanged(nameof(Skill));
            }
        }

        private string m_PlayerDescription;
        public string PlayerDescription
        {
            get { return m_PlayerDescription; }
            set
            {
                if (m_PlayerDescription == value) return;
                m_PlayerDescription = value;
                OnPropertyChanged(nameof(PlayerDescription));
            }
        }
        private ObservableCollection<GameModel> m_Games;
        public ObservableCollection<GameModel> Games
        {
            get { return m_Games; }
            set
            {
                if (m_Games == value) return;
                m_Games = value;
                OnPropertyChanged(nameof(Games));
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

                Image = Xamarin.Forms.ImageSource.FromStream(
                    () => new MemoryStream(Convert.FromBase64String(imageBase64)));
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

        public string Text { get => string.Format("{0} {1}", FirstName, LastName); }
        public string Initials { get => FirstName.Substring(0, 1) + LastName.Substring(0, 1); }
    }
}