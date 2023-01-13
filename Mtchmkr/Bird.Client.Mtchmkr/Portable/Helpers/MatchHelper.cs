//using System.Drawing;

using Bird.Client.Mtchmkr.Portable.Models;
using System;

namespace Bird.Client.Mtchmkr.Helpers
{
    public class MatchHelper
    {
        private static string[] m_Duration = new string[] { "3-7 Frames", "8-15 Frames", "16+ Frames", "3-7 Frames", "8-15 Frames", "16+ Frames" };
        private static string[] m_Locations = new string[] { "Basildon", "Harlow", "Brentwood", "Romford", "Gidea Park", "Billericay", "Stratford", "Lakeside Thurrock", "Grays" };
        public static IMatch Build(IMatch match)
        {
            Random rnd = new Random();
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0,DateTimeKind.Local);
            int days = rnd.Next(0, 90);
            int location = rnd.Next(0, m_Locations.Length - 1);
            int duration = rnd.Next(0, m_Duration.Length - 1);
            match.MatchKey = System.Guid.NewGuid();
            match.Date = date.AddDays(days);
            match.Day = match.Date.Day;
            match.DayName = match.Date.ActualDayOfWeek();
            match.OccuranceSuffix = match.Date.OccuranceSuffix();
            match.Location = m_Locations[location];
            match.Duration = m_Duration[duration];
            Tuple<int, int> skill = CreateSkills();

            match.MaximumSkills = skill.Item1;
            match.MinimumSkills = skill.Item2;
            return match;
        }
        private static Tuple<int, int> CreateSkills()
        {
            Random rnd = new Random();
            int min = rnd.Next(1, 5);
            int max = rnd.Next(1, 5);
            Tuple<int, int> @return;
            if (min > max)
            {
                @return = Tuple.Create<int, int>(max,min);
            }
            else
            {
                @return = Tuple.Create<int, int>(min,max);
            }
            return @return;
        }
    }
}
