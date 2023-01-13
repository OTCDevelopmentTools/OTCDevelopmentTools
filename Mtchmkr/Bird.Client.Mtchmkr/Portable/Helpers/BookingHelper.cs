using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
//using System.Drawing;

namespace Bird.Client.Mtchmkr.Helpers
{
    public class BookingHelper
    {
        private static Color[] Colours = new Color[] { Color.Red, Color.Green, Color.Pink, Color.Gray, Color.Yellow };
        private static string[] Players = new string[] { "Tucker Jenkins", "Lenny Henry", "Don McLean", "Peter Piper", "Lenny McLean" };
        private static string[] Games = new string[] { "Pool", "Snooker", "9 Ball" };
        public static Appointment Create()
        {
            int daysinmonth = System.DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            Random rnd = new Random();
            int days = rnd.Next(1, daysinmonth);
            int game = rnd.Next(1, Games.Length-1);
            int length = rnd.Next(1, 5);
            int player = rnd.Next(1, Players.Length-1);
            int colour = rnd.Next(1, Colours.Length-1);
            DateTime date = DateTime.Now.AddDays(days);
            Appointment model = new Appointment()
            {
               // Id=Guid.NewGuid(),
                StartDate = date,
                EndDate = date.AddHours(length),
                Detail = Games[game],
                Title = Players[player],
                Color = Colours[colour],
        };
            return model;
        }
    }
}
