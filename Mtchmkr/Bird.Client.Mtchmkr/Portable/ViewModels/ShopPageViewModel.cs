using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bird.Client.Mtchmkr.Portable.Comon;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.Views;
using Plugin.InAppBilling;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class ShopPageViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;
        private readonly IInAppPurchase _iInAppPurchage;

        public ICommand OneMtchCommand { get => new Command(() => OneMtchCommandMethod()); }

        public ICommand FiveMtchCommand { get => new Command(() => FiveMtchCommandMethod()); }

        public ICommand TenMtchCommand { get => new Command(() => TenMtchCommandMethod()); }

        public ShopPageViewModel()
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
            _iInAppPurchage = DependencyService.Get<IInAppPurchase>();
        }

        public async void OneMtchCommandMethod()
        {
            var response = await _iInAppPurchage.GetPurchases(Constants.ProductId5MTCH);
            var prod = response.FirstOrDefault();
            var res = await _iInAppPurchage.Purchase(prod);
            if(res.Count>0)
            {
                var tns = res.FirstOrDefault();
                await App.Current.MainPage.DisplayAlert("Alert",tns.ToString(),"Ok");
            }
        }

        public async Task<bool> PurchaseItem(string productId)
        {
           
            return false;
        }

        public async void FiveMtchCommandMethod()
        {
            //_progDialog.ShowProgress("Loading...");
            //var service = DependencyService.Get<IiZettleService>();
            //await service.ChargeAmountAsync(3.99, "USD", Guid.NewGuid().ToString())
            //       .ContinueWith(ChargeFinished).ConfigureAwait(true);
            //_progDialog.HideProgress();

            await App.Current.MainPage.Navigation.PushAsync(new PaymentPage(Convert.ToDecimal(3.99), 5), true);
        }

        public async void TenMtchCommandMethod()
        {
            //_progDialog.ShowProgress("Loading...");
            //var service = DependencyService.Get<IiZettleService>();
            //await service.ChargeAmountAsync(6.99, "USD", Guid.NewGuid().ToString())
            //       .ContinueWith(ChargeFinished).ConfigureAwait(true);
            //_progDialog.HideProgress();

            await App.Current.MainPage.Navigation.PushAsync(new PaymentPage(Convert.ToDecimal(6.99), 10), true);
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