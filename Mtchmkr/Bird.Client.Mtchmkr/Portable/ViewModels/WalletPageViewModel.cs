using System;
using System.Collections.ObjectModel;
using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class WalletPageViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;

        private ObservableCollection<PaymentInfo> paymentInfoItemsSource;
        public ObservableCollection<PaymentInfo> PaymentInfoItemsSource
        {
            get
            {
                return paymentInfoItemsSource;
            }
            set
            {
                paymentInfoItemsSource = value;
                OnPropertyChanged();
            }
        }

        private bool isNoDataView = false;
        public bool IsNoDataView
        {
            get
            {
                return isNoDataView;
            }
            set
            {
                isNoDataView = value;
                OnPropertyChanged();
            }
        }

        public WalletPageViewModel()
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
            GetPaymentInfoMethod();
        }



        public async void GetPaymentInfoMethod()
        {
            _progDialog.ShowProgress("Loading...");
            var userId = Guid.Parse(Preferences.Get("UserId", string.Empty));
            var result = await App.ServiceManager.GetPaymentInfoDataAsync(userId);
            if (result != null)
            {
                PaymentInfoItemsSource = new ObservableCollection<PaymentInfo>(result);
                IsNoDataView = false;
            }
            else
            {
                PaymentInfoItemsSource = new ObservableCollection<PaymentInfo>();
                IsNoDataView = true;
            }

            _progDialog.HideProgress();
        }
    }
}

        
 