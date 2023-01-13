using Bird.Client.Mtchmkr.Portable.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}