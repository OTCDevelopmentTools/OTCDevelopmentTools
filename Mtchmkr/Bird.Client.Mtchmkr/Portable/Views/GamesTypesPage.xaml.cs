using Bird.Client.Mtchmkr.Portable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamesTypesPage : ContentPage
    {
        public GamesTypesPage():base()
        {
            InitializeComponent();
            BindingContext = new GameTypesViewModel();
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            GameTypesViewModel viewModel = (GameTypesViewModel)BindingContext;
        }
    }
}