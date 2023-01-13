using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreferencesFlyout : ContentPage
    {
        public ListView ListView;

        public PreferencesFlyout()
        {
            InitializeComponent();

            BindingContext = new PreferencesFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class PreferencesFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<PreferencesFlyoutMenuItem> MenuItems { get; set; }

            public PreferencesFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<PreferencesFlyoutMenuItem>(new[]
                {
                    new PreferencesFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new PreferencesFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new PreferencesFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new PreferencesFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new PreferencesFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}