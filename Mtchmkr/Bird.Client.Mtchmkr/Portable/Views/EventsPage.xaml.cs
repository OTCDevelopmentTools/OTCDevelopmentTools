using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        public ObservableCollection<EventModel> Items { get; set; }

        public EventsPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<EventModel>();
            foreach(var @event in EventModel.CreateEvents())
            {
                Items.Add(@event);
            }
//            MyListView.ItemsSource = Items;
            BindingContext = new EventsViewModel();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
