using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bird.Client.Mtchmkr.iOS.DependencyServices;
using Bird.Client.Mtchmkr.Portable.Comon;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Foundation;
using StoreKit;
using Xamarin.Forms;
[assembly:Dependency(typeof(InAppPurchase))]
namespace Bird.Client.Mtchmkr.iOS.DependencyServices
{
    public class InAppPurchase : IInAppPurchase
    {
        public Task<List<Purchase>> GetPurchases(params string[] ids)
        {
            TaskCompletionSource<List<Purchase>> taskCompletionSource = new TaskCompletionSource<List<Purchase>>();
            var req = new SKProductsRequest(new NSSet(ids));
            req.Delegate = new ProductRequestDelegate((items) =>
            {
                taskCompletionSource.SetResult(items);
            });
            req.Start();
            return taskCompletionSource.Task;
        }

        public Task<List<Reciept>> Purchase(Purchase purchase)
        {
            TaskCompletionSource<List<Reciept>> taskCompletionSource = new TaskCompletionSource<List<Reciept>>();
            TransactionObserver _observer=new TransactionObserver((tns) =>
            {
                taskCompletionSource.SetResult(tns);
            });
            var prod = (SKProduct)purchase.NativeObject;
            var payment = SKPayment.CreateFrom(prod);
            var source = new TaskCompletionSource<Reciept>();
            SKPaymentQueue.DefaultQueue.AddPayment(payment);
            SKPaymentQueue.DefaultQueue.AddTransactionObserver(_observer);
            return taskCompletionSource.Task;
        }
    }
    class ProductRequestDelegate : SKProductsRequestDelegate
    {
        private Action<List<Purchase>> _onReponse;

        public ProductRequestDelegate(Action<List<Purchase>> onResponse)
        {
            _onReponse = onResponse;
        }
        public override void ReceivedResponse(SKProductsRequest request, SKProductsResponse response)
        {
            var items = new List<Purchase>();
            foreach (var item in response.Products)
            {
                items.Add(new Purchase
                {
                    Id = item.ProductIdentifier,
                    Description = item.Description,
                    Price = item.Price.DoubleValue,
                    LocalizeTitle=item.LocalizedTitle,
                    NativeObject=item
                });
            }
            _onReponse.Invoke(items);
        }
    }
    class TransactionObserver : ISKPaymentTransactionObserver
    {
        private Action<List<Reciept>> _action;

        public TransactionObserver(Action<List<Reciept>>  action)
        {
            _action = action;
        }
        public IntPtr Handle => IntPtr.Zero;

        public void Dispose()
        {
            Dispose();
        }
        public void UpdatedTransactions(SKPaymentQueue queue, SKPaymentTransaction[] transactions)
        {
            var rec = new List<Reciept>();
            foreach (var item in transactions)
            {
                PurchaseState state;
                switch(item.TransactionState)
                {
                    case SKPaymentTransactionState.Failed:
                         state = PurchaseState.Failed;
                         break;
                    case SKPaymentTransactionState.Purchasing:
                        state = PurchaseState.Purchasing;
                        break;
                    case SKPaymentTransactionState.Purchased:
                        state = PurchaseState.Purchased;
                        break;
                    default:
                        state = PurchaseState.Failed;
                        break;
                }
                rec.Add(new Reciept
                {
                    PurchasedState = state,
                    Description = item.Description,
                    Error = item.Error.ToString(),
                    Id = item.Payment.ProductIdentifier.ToString(),
                });
                _action.Invoke(rec);
            }
        }
    }
}

