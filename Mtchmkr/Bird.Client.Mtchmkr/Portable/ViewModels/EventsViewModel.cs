using Bird.Client.Mtchmkr.Portable.Models;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class EventsViewModel : BaseViewModel
    {
        public EventsViewModel()
        {
            Title = "Events";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

            Events = EventModel.CreateEvents();
        }

        public EventModel[] Events { get; set; }
        public ICommand OpenWebCommand { get; }
    }
}