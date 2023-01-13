using System;
using System.Collections.Generic;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public partial class WalletPage : ContentPage
    {
        public WalletPage()
        {
            InitializeComponent();
            this.BindingContext = new WalletPageViewModel();
        }
    }
}
