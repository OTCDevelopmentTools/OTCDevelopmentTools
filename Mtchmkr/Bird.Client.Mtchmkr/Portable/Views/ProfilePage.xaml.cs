using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage():this(new BasePlayerModel { PlayerKey=Guid.NewGuid(), FirstName = "Andrew ", LastName = "Bird", PlayerImage ="AB.jpg" })
        {
           // InitializeComponent();
        }

        public ProfilePage(BasePlayerModel profile)
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (this.BindingContext as ProfileViewModel).GetProfile();
        }
    }
}