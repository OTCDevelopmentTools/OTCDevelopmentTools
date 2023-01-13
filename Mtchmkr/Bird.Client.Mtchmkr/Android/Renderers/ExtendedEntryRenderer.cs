using System.ComponentModel;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Bird.Client.Mtchmkr.Android.Renderers;
using Bird.Client.Mtchmkr.Portable.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace Bird.Client.Mtchmkr.Android.Renderers
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        ExtendedEntry TextBox;

        public ExtendedEntryRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            TextBox = Element as ExtendedEntry;
            if (e.OldElement == null)
            {
                if (TextBox.ReturnKeyType == ReturnKeyTypes.Done)
                {
                    Control.ImeOptions = ImeAction.Done;
                    Control.EditorAction += (object sender, TextView.EditorActionEventArgs args) =>
                    {
                        if (TextBox.ReturnKeyType != ReturnKeyTypes.Next)
                        {
                            TextBox.Unfocus();
                        }
                        TextBox.InvokeCompleted();
                    };
                }
                else if (TextBox.ReturnKeyType == ReturnKeyTypes.Next)
                {
                    if (TextBox.IsEnabled)
                    {
                        Control.ImeOptions = ImeAction.Next;
                        Control.EditorAction += (object sender, global::Android.Widget.TextView.EditorActionEventArgs ex) =>
                        {
                            ex.Handled = false;
                        };
                    }
                }


                SetBorder(TextBox, TextBox.BorderColor, TextBox.BorderWidth, TextBox.CornerRadius);

                if (TextBox.HasPin)
                    Control.Gravity = GravityFlags.CenterVertical;
                else
                    Control.Gravity = GravityFlags.CenterHorizontal;

                Control.SetPadding(15, 10, 10, 10);

                Control.SetCursorVisible(true);
                float dpi = Resources.DisplayMetrics.Density;
                Control.SetSelection(Control.Text.Length);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            SetBorder(TextBox, TextBox.BorderColor, TextBox.BorderWidth, TextBox.CornerRadius);

            if (TextBox.HasPin)
                Control.Gravity = GravityFlags.CenterVertical;
            else
                Control.Gravity = GravityFlags.CenterHorizontal;

            Control.SetPadding(15, 10, 10, 10);

            Control.SetCursorVisible(true);
            float dpi = Resources.DisplayMetrics.Density;
            Control.SetSelection(Control.Text.Length);
        }


        void SetBorder(ExtendedEntry extendedEntry, Color _BorderColor, double _BorderWidth, double _CornerRadius)
        {
            GradientDrawable gd = new GradientDrawable();
            if (extendedEntry.HasBorder)
            {
                gd.SetColor(Color.Black.ToAndroid());
                gd.SetStroke((int)_BorderWidth, _BorderColor.ToAndroid());
            }
            else
            {
                gd.SetColor(Color.Transparent.ToAndroid());
                gd.SetStroke((int)_BorderWidth, Color.Transparent.ToAndroid());
            }

            gd.SetCornerRadius((float)_CornerRadius);
            this.Control.SetBackground(gd);
        }
    }
}