using Bird.Client.Mtchmkr.Portable.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    internal class ExtendedContentPage<T> :ContentPage where T : BaseViewModel
    {
        private BaseViewModel m_ViewModel;

        public ExtendedContentPage(T viewModel)
        {
            m_ViewModel = viewModel;    
            BindingContext = m_ViewModel;   
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            m_ViewModel.OnAppearing();
        }
    }
}
