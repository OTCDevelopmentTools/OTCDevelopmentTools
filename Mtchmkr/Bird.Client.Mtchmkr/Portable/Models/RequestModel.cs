using Bird.Client.Mtchmkr.Helpers;
using System;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class RequestModel: BaseModel
    {
        public Guid Key { get; set; }
        public int Rating { get; set; }
        private string m_Name;
        public string Name
        {
            get { return m_Name; }
            set
            {
          //      if (m_Requestor == value) return;
                m_Name = value;
          //      OnPropertyChanged(nameof(Requestor));
            }
        }
        private bool m_Expanded;

        public bool Expanded
        {
            get { return m_Expanded; }
            set
            {
                if (m_Expanded == value) return;
                m_Expanded = value;
                OnPropertyChanged(nameof(Expanded));
            }
        }

        public string Day
        {
            get
            {
               // string @return = String.Empty;
                    //      string[] names = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;
                    //     @return = names[(int)Date.DayOfWeek];
                    return Date.ActualDayOfWeek();
            }
        }
        public Color Colour { get; set; } = Color.Red;

        public string ShortDate
        {
            get
            {
                string @return = string.Empty;
                try
                {
                    string[] names = DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames;
                    @return = string.Format("{1} {0}", names[Date.Month] , Date.Day);
                }
                catch (Exception ex)
                {

                }
                return @return;
            }
        }
        public string Initials
        {
            get => Naming.CreateInitials(Name);
        }
        public string Location { get; set; }
        public string Image { get; set; }

        private bool m_Booked;
        public bool Booked
        {
            get { return m_Booked; }
            set
            {
                if (m_Booked == value) return;
                m_Booked = value;
                OnPropertyChanged(nameof(Booked));
            }
        }

        public string Game { get; set; }
        public int Skills { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        
    }
}