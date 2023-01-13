using System;

namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class GameModel:BaseModel
    {
        public Guid Key { get; set; }
        private string m_Name;
        public string GameName
        {
            get { return m_Name; }
            set
            {
                if (m_Name == value) return;
                m_Name = value;
                OnPropertyChanged(nameof(GameName));
            }
        }
        private string m_Description;
        public string Description
        {
            get { return m_Description; }
            set
            {
                if (m_Description == value) return;
                m_Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        private string m_Image;
        public string GameImage
        {
            get { return m_Image; }
            set
            {
                if (m_Image == value) return;
                m_Image = value;
                OnPropertyChanged(nameof(GameImage));
            }
        }


    }
}