using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.Services;
using Braintree;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class PaymentPageViewModel
    {
        private readonly IProgressDialog _progDialog;

        public ICommand PayCommand { get; set; }
        public CardInfo CardInfo { get; set; } = new CardInfo();
        IPayService _payService;
        BraintreeGateway gateway;
        decimal _amount;
        int _match;

        public PaymentPageViewModel(decimal amount, int match)
        {
            _amount = amount;
            _match = match;
            _progDialog = DependencyService.Get<IProgressDialog>();
            _payService = Xamarin.Forms.DependencyService.Get<IPayService>();
            PayCommand = new Command(async () => await CreatePayment());


            gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "2n443gxqxs9rkksw",
                PublicKey = "c8zhdkr7fsgk7jvz",
                PrivateKey = "ab0369bfc38d6f4ccd5542bdb0509b62"
            };

            var clientToken = gateway.ClientToken.Generate();

            PayCommand = new Command(async () => await CreatePayment());
            GetPaymentConfig(clientToken);
        }

        async Task GetPaymentConfig(string token)
        {

            await _payService.InitializeAsync(token);

        }

        async Task CreatePayment()
        {
            _progDialog.ShowProgress("Loading...");

            if (_payService.CanPay)
            {
                try
                {
                    _payService.OnTokenizationSuccessful += OnTokenizationSuccessful;
                    _payService.OnTokenizationError += OnTokenizationError;
                    await _payService.TokenizeCard(CardInfo.CardNumber.Replace(" ", string.Empty), CardInfo.Expiry.Substring(0, 2), $"{DateTime.Now.ToString("yyyy").Substring(0, 2)}{CardInfo.Expiry.Substring(3, 2)}", CardInfo.Cvv);

                }
                catch (Exception ex)
                {
                    _progDialog.HideProgress();
                    await App.Current.MainPage.DisplayAlert("Error", "Unable to process payment", "Ok");
                    System.Diagnostics.Debug.WriteLine(ex);
                }

            }
            else
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    _progDialog.HideProgress(); 
                    await App.Current.MainPage.DisplayAlert("Error", "Payment not available", "Ok");
                });
            }
        }

        async void OnTokenizationSuccessful(object sender, string e)
        {
            _payService.OnTokenizationSuccessful -= OnTokenizationSuccessful;
            System.Diagnostics.Debug.WriteLine($"Payment Authorized - {e}");
            
            await App.Current.MainPage.DisplayAlert("Success", $"Payment Authorized: the token is{e}", "Ok");

            var result = await MakePayment(_amount);
        }

        async void OnTokenizationError(object sender, string e)
        {
            _payService.OnTokenizationError -= OnTokenizationError;
            System.Diagnostics.Debug.WriteLine(e);
            _progDialog.HideProgress();
            await App.Current.MainPage.DisplayAlert("Error", "Unable to process payment", "Ok");

        }

        public async Task<bool> MakePayment(decimal amount)
        {
            _progDialog.ShowProgress("Loading...");

            var nonce = "fake-valid-nonce";
            var request = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                Transaction transaction = result.Target;
                await App.Current.MainPage.DisplayAlert("Show", "Transaction Id : " + transaction.Id, "Ok");

                PaymentRequest requestPaymentInfo = new PaymentRequest();
                requestPaymentInfo.matchCount = _match;
                requestPaymentInfo.payment = Convert.ToDouble(_amount);
                requestPaymentInfo.transactionId = transaction.Id;
                requestPaymentInfo.userId = Guid.Parse(Preferences.Get("UserId", string.Empty));
                requestPaymentInfo.createdDate = DateTime.Now;

                var resultPaymentInfo = await App.ServiceManager.InsertPaymentInfoAsync(requestPaymentInfo);

                _progDialog.HideProgress();
                App.Current.MainPage.Navigation.PopAsync();
                return true;
            }
            else if (result.Transaction != null)
            {
                await App.Current.MainPage.DisplayAlert("Show", "Transaction Id : " + result.Transaction.Id, "Ok");
                _progDialog.HideProgress();
                App.Current.MainPage.Navigation.PopAsync();
                return false;
            }
            else
            {
                string errorMessages = "";
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
                }
                await App.Current.MainPage.DisplayAlert("Show", errorMessages, "Ok");
                _progDialog.HideProgress();
                App.Current.MainPage.Navigation.PopAsync();
                return false;
            }

            

        }

    }


    public class CardInfo
    {
        public string CardNumber { get; set; }
        public string Expiry { get; set; }
        public string Cvv { get; set; }
    }

}