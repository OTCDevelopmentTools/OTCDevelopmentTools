using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidHUD;
using Bird.Client.Mtchmkr.Android.DependencyServices;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgressDialogService))]
namespace Bird.Client.Mtchmkr.Android.DependencyServices
{
    public class ProgressDialogService : IProgressDialog
    {
        public bool IsShowing { get; set; }

        #region IProgressDialog implementation

        public void ShowProgress(string message)
        {
            try
            {
                IsShowing = true;
                AndHUD.Shared.Show(MainActivity.ShareActivityContext, message, -1, MaskType.Black);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProgressDialogService Android : " + ex.Message);
            }
        }

        public void HideProgress()
        {
            IsShowing = false;
            AndHUD.Shared.Dismiss(MainActivity.ShareActivityContext);
        }

        public void ShowProgressAlt(string message)
        {
            ShowProgress(message);
        }

        #endregion
    }
}
