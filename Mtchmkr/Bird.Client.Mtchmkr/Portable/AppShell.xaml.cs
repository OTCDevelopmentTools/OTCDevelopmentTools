using Bird.Client.Mtchmkr.Portable.ViewModels;
using Bird.Client.Mtchmkr.Portable.Views;
using Bird.Client.Mtchmkr.Portable.Helpers;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Bird.Client.Mtchmkr.Helpers;

namespace Bird.Client.Mtchmkr
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        
        public AppShell()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //    <MenuItem IconImageSource="Admin.png" Text="Admin" Command="{Binding AdminCommand}"></MenuItem>

            AppShellViewModel viewModel;
            BindingContext = viewModel= new AppShellViewModel(this);

            //this.Items.Add(new MenuItem
            //{
            //    Text="Admino",
            //    IconImageSource = "Admin.png",
            //    Command = viewModel.AdminCommand,
            //});
            
            //TODO
        }


    }
}
