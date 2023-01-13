using Bird.Client.Mtchmkr.Portable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Header : ContentView
    {
        public static BindableProperty FirstNameProperty = BindableProperty.Create(nameof(FirstName),
    typeof(string), typeof(Header), default(string),
    defaultBindingMode: BindingMode.OneWay);

        public static BindableProperty LastNameProperty = BindableProperty.Create(nameof(LastName),
    typeof(string), typeof(Header), default(string),
    defaultBindingMode: BindingMode.OneWay);

        public static BindableProperty CaptionProperty = BindableProperty.Create(nameof(Caption),
    typeof(string), typeof(Header), default(string),
    defaultBindingMode: BindingMode.OneWay);

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        public string LastName 
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        public Header()
        {
            InitializeComponent();
            BindingContext = this;
            FirstNameControl.SetBinding(Span.TextProperty, new Binding(nameof(FirstName), BindingMode.OneWay));
            LastNameControl.SetBinding(Span.TextProperty, new Binding(nameof(LastName), BindingMode.OneWay));
            CaptionControl.SetBinding(Label.TextProperty, new Binding(nameof(Caption), BindingMode.OneWay));
        }
    }
}