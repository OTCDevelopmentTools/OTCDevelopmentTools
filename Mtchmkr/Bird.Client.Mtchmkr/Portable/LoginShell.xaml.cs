using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginShell : Shell
    {
        public LoginShell()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
  //          CurrentItem = Items[1];
        }
    }
}