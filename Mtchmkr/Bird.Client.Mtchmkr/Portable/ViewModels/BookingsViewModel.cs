//using System.Drawing;

using Bird.Client.Mtchmkr.Helpers;
using Bird.Client.Mtchmkr.Portable.Models;
using Telerik.XamarinForms.Input;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class BookingsViewModel : CollectionViewModel<Appointment>
    {
        RadCalendar m_Calendar;
        RadComboBox m_ComboBox;
        public BookingsViewModel(RadCalendar calendar )
        {
            m_Calendar = calendar;
            m_Calendar.ViewChanged += Calendar_ViewChanged;
            m_Calendar.SelectedDate = DateTime.Now;

//            m_Calendar.AppointmentsSource = Collection;
        }

        public override void Dispose()
        {
            base.Dispose();
            if (null != m_Calendar)
            {
                m_Calendar.ViewChanged -= Calendar_ViewChanged;
            }
        }
        private void Calendar_ViewChanged(object sender, Telerik.XamarinForms.Common.ValueChangedEventArgs<CalendarViewMode> e)
        {

            string text = e.NewValue.ToString();
            if (SelectedItem!= text)
            {
                SelectedItem = text;
            }

        }

        protected override void LoadData()
        {
            base.LoadData();
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
            Collection.Add(BookingHelper.Create());
        }

        private CalendarViewMode m_CalendarViewMode = CalendarViewMode.Day;
        public CalendarViewMode CalendarViewMode
        {
            get { return m_CalendarViewMode; }
            set
            {
                if (m_CalendarViewMode == value) return;
                m_CalendarViewMode = value;
                OnPropertyChanged(nameof(CalendarViewMode));
                m_ComboBox.Text = value.ToString();
            }
        }
        private string m_SelectedItem;
        public string SelectedItem
        {
            get
            {
                return this.m_SelectedItem;
            }
            set
            {
                if (this.m_SelectedItem != value)
                {
                    this.m_SelectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    m_Calendar.ViewMode = FindMode(value);
                }
            }
        }

        CalendarViewMode FindMode(string mode)
        {
            CalendarViewMode @enum = Enum.Parse<CalendarViewMode>(mode);
            return @enum;
        }
        public string[] ViewTypes
        {
            get
            {
                List<string> stuff = new List<string>();
                stuff.AddRange(Enum.GetNames(typeof(CalendarViewMode)));
                stuff.Remove("Century");
                return stuff.ToArray();
            }
        }

        public ICommand DayViewCommand { get => new Command(() => View(CalendarViewMode.Day)); }
        public ICommand WeekViewCommand { get => new Command(() => View(CalendarViewMode.Week)); }
        public ICommand MonthViewCommand { get => new Command(() => View(CalendarViewMode.Month)); }

        public void View(CalendarViewMode view)
        {
            m_Calendar.ViewMode = view;
        }
    }
}