using System;
using System.Collections.Generic;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public partial class ResetPasswordPage : ContentPage
    {
        public ResetPasswordPage()
        {
            InitializeComponent();
            this.BindingContext = new ResetPasswordViewModel();
        }
    }
}
