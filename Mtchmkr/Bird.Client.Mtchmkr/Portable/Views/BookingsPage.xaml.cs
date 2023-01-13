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
    public partial class BookingsPage : ContentPage
    {
        public BookingsPage()
        {
            
            try
            {
                InitializeComponent();
                BindingContext = new BookingsViewModel(calendar);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        } 
    }
}