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
    public partial class ForgotPasswordPage : ContentPage
    {

            public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Caption { get; set; }
        public ForgotPasswordPage()
        {
            InitializeComponent();
            this.BindingContext = new ForgotPasswordViewModel();
        }
    }
}