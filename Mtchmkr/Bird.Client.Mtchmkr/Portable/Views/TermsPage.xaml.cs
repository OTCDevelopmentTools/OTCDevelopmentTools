using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsPage : ContentPage
    {
        HtmlWebViewSource source;

        public TermsPage()
        {
            InitializeComponent();

            BindingContext = new TermsViewModel();

            source = new HtmlWebViewSource();
            string url = DependencyService.Get<IBaseUrl>().Get();
            string TempUrl = Path.Combine(url, "MmTermsNew.html");
            source.BaseUrl = url;

            try
            {
                ReadFileMethod("MmTermsNew.html");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            webView.BackgroundColor = Color.Transparent;
            webView.Source = source;
        }

        async void ReadFileMethod(string fileName)
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync(fileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    var fileContents = await reader.ReadToEndAsync();
                    source.Html = fileContents;
                }
            }
        }
    }
}