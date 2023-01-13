using System;
using System.Collections.Generic;

namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class EventModel
    {
        public Guid Key { get; set; }
        public string Game { get; set; }
        public string GameImage { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Player1Image { get; set; }
        public string Player2Image { get; set; }

        public static EventModel[] CreateEvents()
        {
            List<EventModel> _Events = new List<EventModel>();
            _Events.Add(new EventModel { Key=Guid.NewGuid() , Game="Pool" , GameImage="*Ball.png", Player1 = "Andrew Bird", Player1Image = "AB.jpg", Player2 = "James Boughton", Player2Image = "JB.jpg" });
            return _Events.ToArray();
        }
    }
}