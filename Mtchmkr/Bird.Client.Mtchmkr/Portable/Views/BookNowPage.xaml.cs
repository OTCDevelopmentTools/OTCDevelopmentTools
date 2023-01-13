using Bird.Client.Mtchmkr.Portable.ViewModels;
using Bird.Client.Mtchmkr.Portable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookNowPage : ContentPage
    {
        public BookNowPage(ShowCaseResponse model)
        {
            InitializeComponent();
            BindingContext = new BookNowViewModel(Navigation, model);
        }
    }
}