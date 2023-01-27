using System;
using Bird.Client.Mtchmkr.iOS.DependencyServices;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl_iOS))]
namespace Bird.Client.Mtchmkr.iOS.DependencyServices
{
    public class BaseUrl_iOS : IBaseUrl
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }

        public string GetIdentifier()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.ToString();
        }
    }
}
