using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public partial class NewRequestPage : ContentPage
    {
        public RequestModel Request { get; set; }

        public NewRequestPage()
        {
            InitializeComponent();
            BindingContext = new RequestModel();
        }
    }
}