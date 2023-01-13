using System;
using System.Collections.Generic;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public partial class ShopPage : ContentPage
    {
        public ShopPage()
        {
            InitializeComponent();
            this.BindingContext = new ShopPageViewModel();
        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            this.Navigation.PushAsync(new WalletPage(), true);
        }
    }
}
