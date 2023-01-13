using System;

namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class PendingModel : BasePlayerModel , IMatch , IGame , IPlayer
    {
        public Guid MatchKey { get; set; }
        public Guid GameKey { get; set; }
        public Guid PlayerKey { get; set; }
        public string DayName { get; set; }
        public string OccuranceSuffix { get; set; }
        public int Day { get; set; }
        //private string m_PlayerImage;
        //public string PlayerImage
        //{
        //    get { return m_PlayerImage; }
        //    set
        //    {
        //        if (m_PlayerImage == value) return;
        //        m_PlayerImage = value;
        //        OnPropertyChanged(nameof(PlayerImage));
        //    }
        //}
        private int m_MinimumSkills;
        private int m_MaximumSkills;
        private string m_Location;
        private string m_Duration;
        public string Duration
        {
            get { return m_Duration; }
            set
            {
                if (m_Duration == value) return;
                m_Duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        public string Location
        {
            get { return m_Location; }
            set
            {
                if (m_Location == value) return;
                m_Location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public int MaximumSkills
        {
            get { return m_MaximumSkills; }
            set
            {
                if (m_MaximumSkills == value) return;
                m_MaximumSkills = value;
                OnPropertyChanged(nameof(MaximumSkills));
            }
        }

        public int MinimumSkills
        {
            get { return m_MinimumSkills; }
            set
            {
                if (m_MinimumSkills == value) return;
                m_MinimumSkills = value;
                OnPropertyChanged(nameof(MinimumSkills));
            }
        }
        private int m_PlayerSkill;

        public int PlayerSkill
        {
            get { return m_PlayerSkill; }
            set
            {
                if (m_PlayerSkill == value) return;
                m_PlayerSkill = value;
                OnPropertyChanged(nameof(PlayerSkill));
            }
        }

        private string m_GameName;
        public string GameName
        {
            get { return m_GameName; }
            set
            {
                if (m_GameName == value) return;
                m_GameName = value;
                OnPropertyChanged(nameof(GameName));
            }
        }
        private string m_PlayerName;

        public string PlayerName
        {
            get { return m_PlayerName; }
            set
            {
                if (m_PlayerName == value) return;
                m_PlayerName = value;
                OnPropertyChanged(nameof(PlayerName));
            }
        }
        private string m_GameImage;

        public string GameImage
        {
            get { return m_GameImage; }
            set
            {
                if (m_GameImage == value) return;
                m_GameImage = value;
                OnPropertyChanged(nameof(GameImage));
            }
        }

        private DateTime m_Date;
        public DateTime Date
        {
            get { return m_Date; }
            set
            {
                if (m_Date == value) return;
                m_Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }


    }
}