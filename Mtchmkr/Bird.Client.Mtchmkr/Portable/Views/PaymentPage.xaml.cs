using System;
using System.Collections.Generic;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public partial class PaymentPage : ContentPage
    {
        public PaymentPage(decimal amount, int match)
        {
            InitializeComponent();
            this.BindingContext = new PaymentPageViewModel(amount, match);
        }
    }
}
