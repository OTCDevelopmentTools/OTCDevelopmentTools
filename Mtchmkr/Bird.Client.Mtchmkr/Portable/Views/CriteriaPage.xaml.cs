using Bird.Client.Mtchmkr.Portable.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public partial class CriteriaPage : ContentPage
    {
        public CriteriaPage()
        {
            InitializeComponent();
            BindingContext = new CriteriaViewModel();
        }
    }
}