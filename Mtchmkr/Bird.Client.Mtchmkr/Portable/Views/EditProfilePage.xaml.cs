using System;
using System.Collections.Generic;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public partial class EditProfilePage : ContentPage
    {
        public EditProfilePage(UserProfileResponse ProfileData)
        {
            InitializeComponent();
            this.BindingContext = new EditProfilePageViewModel(ProfileData);
        }
    }
}
