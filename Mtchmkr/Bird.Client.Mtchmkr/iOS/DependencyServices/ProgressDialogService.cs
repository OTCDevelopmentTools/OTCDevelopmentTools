using BigTed;
using Bird.Client.Mtchmkr.iOS.DependencyServices;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgressDialogService))]
namespace Bird.Client.Mtchmkr.iOS.DependencyServices
{
    public class ProgressDialogService : IProgressDialog
    {

        public bool IsShowing { get; set; }

        #region IProgressDialog implementation

        public void ShowProgressAlt(string message)
        {
            IsShowing = true;
            Task.Run(() => BTProgressHUD.Show(message, -1, ProgressHUD.MaskType.Gradient));
        }

        public void ShowProgress(string message)
        {
            IsShowing = true;
            UIApplication.SharedApplication.InvokeOnMainThread(() => BTProgressHUD.Show(message, -1, ProgressHUD.MaskType.Gradient));
        }

        public void HideProgress()
        {
            IsShowing = false;
            BTProgressHUD.Dismiss();
        }

        #endregion
    }
}