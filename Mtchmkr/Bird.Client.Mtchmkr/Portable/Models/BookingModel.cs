using System;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class BookingModel:BaseModel
    {
        public Guid Key { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    //    public string Game { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public Color Color { get; set; }
    }
}