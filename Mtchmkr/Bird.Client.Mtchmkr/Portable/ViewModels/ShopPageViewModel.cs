using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.Views;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class ShopPageViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;

        

        public ICommand OneMtchCommand { get => new Command(() => OneMtchCommandMethod()); }

        public ICommand FiveMtchCommand { get => new Command(() => FiveMtchCommandMethod()); }

        public ICommand TenMtchCommand { get => new Command(() => TenMtchCommandMethod()); }

        public ShopPageViewModel()
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
        }

        public async void OneMtchCommandMethod()
        {
           // _progDialog.ShowProgress("Loading...");
           // var service = DependencyService.Get<IiZettleService>();
           //await service.ChargeAmountAsync(0.99, "USD", Guid.NewGuid().ToString())
           //        .ContinueWith(ChargeFinished).ConfigureAwait(true);
           // _progDialog.HideProgress();

            await App.Current.MainPage.Navigation.PushAsync(new PaymentPage(Convert.ToDecimal(0.99),1), true);
        }

        public async void FiveMtchCommandMethod()
        {
            //_progDialog.ShowProgress("Loading...");
            //var service = DependencyService.Get<IiZettleService>();
            //await service.ChargeAmountAsync(3.99, "USD", Guid.NewGuid().ToString())
            //       .ContinueWith(ChargeFinished).ConfigureAwait(true);
            //_progDialog.HideProgress();

            await App.Current.MainPage.Navigation.PushAsync(new PaymentPage(Convert.ToDecimal(3.99),5), true);
        }

        public async void TenMtchCommandMethod()
        {
            //_progDialog.ShowProgress("Loading...");
            //var service = DependencyService.Get<IiZettleService>();
            //await service.ChargeAmountAsync(6.99, "USD", Guid.NewGuid().ToString())
            //       .ContinueWith(ChargeFinished).ConfigureAwait(true);
            //_progDialog.HideProgress();

            await App.Current.MainPage.Navigation.PushAsync(new PaymentPage(Convert.ToDecimal(6.99),10), true);
        }

        void ChargeFinished(Task<PaymentInfo> task)
        {
            Device.BeginInvokeOnMainThread(() => {
                if (task.IsFaulted)
                {
                    App.Current.MainPage.DisplayAlert("ERROR", task.Exception.InnerException?.Message ?? task.Exception.Message, "Ok");

                    return;
                }

                if (task.IsCanceled)
                {
                    App.Current.MainPage.DisplayAlert("INFO", "Payment is cancelled", "Ok");

                    return;
                }

                App.Current.MainPage.DisplayAlert("INFO", $"Payment completed: {task.Result.ReferenceNumber}", "Ok");
            });
        }

    }
}