using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public class AdminContentPage:ContentPage
    {
        public AdminContentPage()
        {
            App.AdminCheck();
        }
    }
}
