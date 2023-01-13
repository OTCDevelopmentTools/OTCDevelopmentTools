using System;
using System.Collections.ObjectModel;

namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class ProfileModel :BasePlayerModel
    {
        //private string m_Name;
        //public string Name
        //{
        //    get { return m_Name; }
        //    set
        //    {
        //        if (m_Name == value) return;
        //        m_Name = value;
        //        OnPropertyChanged(nameof(Name));
        //    }
        //}
        private bool m_IsAdmin;

        public bool IsAdmin
        {
            get { return m_IsAdmin; }
            set
            {
                if (m_IsAdmin == value) return;
                m_IsAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        //private string m_Description;

        //public string Description
        //{
        //    get { return m_Description; }
        //    set
        //    {
        //        if (m_Description == value) return;
        //        m_Description = value;
        //        OnPropertyChanged(nameof(Description));
        //    }
        //}

        //private int m_Skill;
        //public int Skill
        //{
        //    get { return m_Skill; }
        //    set
        //    {
        //        if (m_Skill == value) return;
        //        m_Skill = value;
        //        OnPropertyChanged(nameof(Skill));
        //    }
        //}

        //private string m_Image;
        //public string Image
        //{
        //    get { return m_Image; }
        //    set
        //    {
        //        if (m_Image == value) return;
        //        m_Image = value;
        //        OnPropertyChanged(nameof(Image));
        //    }
        //}

        //private ObservableCollection<GameModel> m_Games;
        //public ObservableCollection<GameModel> Games
        //{
        //    get
        //    {
        //        if (null == m_Games)
        //        {
        //            m_Games = new ObservableCollection<GameModel>();
        //        }
        //        return m_Games; 
        //    }
        //    set
        //    {
        //        if (m_Games == value) return;
        //        m_Games = value;
        //        OnPropertyChanged(nameof(Games));
        //    }
        //}


    }
}