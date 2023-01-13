using System;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.CustomControls
{
    public class ExtendedEntry : Entry
    {
        public new event EventHandler Completed;

        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create("HasBorder", typeof(bool), typeof(ExtendedEntry), true);

        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        public static readonly BindableProperty HasPinProperty =
            BindableProperty.Create("HasPin", typeof(bool), typeof(ExtendedEntry), true);

        public bool HasPin
        {
            get { return (bool)GetValue(HasPinProperty); }
            set { SetValue(HasPinProperty, value); }
        }

        public static readonly BindableProperty NextReturnProperty =
            BindableProperty.Create("ReturnNext", typeof(bool), typeof(ExtendedEntry), true);

        public bool ReturnNext
        {
            get { return (bool)GetValue(NextReturnProperty); }
            set { SetValue(NextReturnProperty, value); }
        }

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create("BorderColor", typeof(Color), typeof(ExtendedEntry), Color.Black);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create("BorderWidth", typeof(double), typeof(ExtendedEntry), 1.0);

        public double BorderWidth
        {
            get { return (double)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create("CornerRadius", typeof(double), typeof(ExtendedEntry), 1.0);

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnKeyType), typeof(ReturnKeyTypes), typeof(ExtendedEntry), ReturnKeyTypes.Done);

        public ReturnKeyTypes ReturnKeyType
        {
            get { return (ReturnKeyTypes)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }

        public void InvokeCompleted()
        {
            if (this.Completed != null)
                this.Completed.Invoke(this, null);
        }
    }

    public enum ReturnKeyTypes : int
    {
        Default,
        Go,
        Google,
        Join,
        Next,
        Route,
        Search,
        Send,
        Yahoo,
        Done,
        EmergencyCall,
        Continue
    }
}