using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using Telerik.XamarinForms.DataControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultsPage : ContentPage
    {
        public SearchResultsPage(ObservableCollection<PlayerDTO> data, Guid locationId, DateTime bookingDate)
        {
            InitializeComponent();
            BindingContext = new SearchResultsViewModel(Navigation);

            (this.BindingContext as SearchResultsViewModel).PlayersItemsSource = data;
            (this.BindingContext as SearchResultsViewModel).LocationId = locationId;
            (this.BindingContext as SearchResultsViewModel).BookingDate = bookingDate;
        }

        void listView_SelectionChanged(System.Object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var list = (sender as RadListView);
            (this.BindingContext as SearchResultsViewModel).SelectedPlayers.Clear();
            if (list.SelectedItems != null && list.SelectedItems.Count > 0)
            {
                foreach (var item in list.SelectedItems)
                {
                    var obj = item as PlayerDTO;

                    (this.BindingContext as SearchResultsViewModel).SelectedPlayers.Add(obj);
                }
                
            }
        }
    }
}