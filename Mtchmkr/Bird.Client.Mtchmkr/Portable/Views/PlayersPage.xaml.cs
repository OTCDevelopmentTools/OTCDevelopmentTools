using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using Bird.Client.Mtchmkr.Portable.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public partial class PlayersPage : ContentPage
    {
        PlayersViewModel _viewModel;

        public PlayersPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new PlayersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}