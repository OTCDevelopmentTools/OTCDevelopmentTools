using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreCardPage : ContentPage
    {
        public ScoreCardPage()
        {
            InitializeComponent();
            this.BindingContext = new ScoreCardViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (this.BindingContext as ScoreCardViewModel).SearchPlayersMethod();
        }
    }
}