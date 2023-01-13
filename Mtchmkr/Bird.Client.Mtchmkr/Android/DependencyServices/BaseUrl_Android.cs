using System;
using Bird.Client.Mtchmkr.Android.DependencyServices;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl_Android))]
namespace Bird.Client.Mtchmkr.Android.DependencyServices
{
    public class BaseUrl_Android : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}