using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.XamarinForms.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidationCode : ContentView
    {
        public static BindableProperty CodeProperty = BindableProperty.Create(nameof(Code),
typeof(string), typeof(Header), default(string),
defaultBindingMode: BindingMode.OneWay,propertyChanged: OnCodePropertyChanged);
         
        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set 
            { 
                SetValue(CodeProperty, value);
                LoadCode();
            }
        }

        private static void OnCodePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (ValidationCode)bindable;
            control.Code = newvalue.ToString(); 
        }

        private string m_Code1;
        public string Code1
        {
            get { return m_Code1; }
            set
            {
                if (m_Code1 == value) return;
                m_Code1 = value;
                OnPropertyChanged(nameof(Code1));
            }
        }

        private string m_Code2;
        public string Code2
        {
            get { return m_Code2; }
            set
            {
                if (m_Code2 == value) return;
                m_Code2 = value;
                OnPropertyChanged(nameof(Code2));
            }
        }

        private string m_Code3;
        public string Code3
        {
            get { return m_Code3; }
            set
            {
                if (m_Code3 == value) return;
                m_Code3 = value;
                OnPropertyChanged(nameof(Code3));
            }
        }

        private string m_Code4;
        public string Code4
        {
            get { return m_Code4; }
            set
            {
                if (m_Code4 == value) return;
                m_Code4 = value;
                OnPropertyChanged(nameof(Code4));
            }
        }



        void LoadCode()
        {
            if (string.IsNullOrEmpty(Code)) return;
            PropertyInfo[] codes = new PropertyInfo[] { FindProperty(nameof(Code1)), FindProperty(nameof(Code2)), FindProperty(nameof(Code3)), FindProperty(nameof(Code4)) };
            string newCode = Code + "    ";
            char[] first4 = newCode.Substring(0, 4).ToCharArray();
            for(int i = 0; i < first4.Length; i++)
            {
                codes[i].SetValue(this, first4[i].ToString());
            }
        }
        private PropertyInfo FindProperty(string name)
        {
            Type type = this.GetType();
            PropertyInfo property = type.GetProperty(name);
            return property;
        }
        public Command PasteCommand { get => new Command(async () => await Paste()); }

        async Task Paste()
        {
            var text = await Clipboard.GetTextAsync();
            Code = text;
        }
        public ValidationCode()
        {
             
            InitializeComponent();
            BindingContext = this;
            //            CaptionControl.SetBinding(Entry.TextProperty, new Binding(nameof(Code), BindingMode.OneWay));
  //          Code1 = "T";
        }
    }
}