using System;

namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class ProposedMatchModel : BasePlayerModel, IPlayer, IMatch,IGame
    {
        public Guid MatchKey { get; set; }
        public Guid GameKey { get; set; }

        private string m_GameName;
        private string m_GameImage;

        private string m_Location;
        private string m_Duration;
        private int m_Day;
        private string m_DayName;

        private string m_OccuranceSuffix;
        public string OccuranceSuffix
        {
            get { return m_OccuranceSuffix; }
            set
            {
                if (m_OccuranceSuffix == value) return;
                m_OccuranceSuffix = value;
                OnPropertyChanged(nameof(OccuranceSuffix));
            }
        }

        public int Day
        {
            get { return m_Day; }
            set
            {
                if (m_Day == value) return;
                m_Day = value;
                OnPropertyChanged(nameof(Day));
            }
        }
        public string DayName
        {
            get { return m_DayName; }
            set
            {
                if (m_DayName == value) return;
                m_DayName = value;
                OnPropertyChanged(nameof(DayName));
            }
        }
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

        private int m_MinimumSkills;
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
        private int m_MaximumSkills;
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
    }
}