using System;
using Bird.Client.Mtchmkr.iOS.Renderers;
using Bird.Client.Mtchmkr.Portable.CustomControls;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace Bird.Client.Mtchmkr.iOS.Renderers
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        public ExtendedEntryRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            var view = (ExtendedEntry)Element;

            SetBorder(view);
            KeybordNaxt(view);

            if (Control != null)
            {
                Control.EditingDidEnd += (object senderObj, EventArgs e1) =>
                {
                    {
                        if (view.ReturnKeyType != ReturnKeyTypes.Next)
                        {
                            view.Unfocus();
                        }
                        view.InvokeCompleted();
                    };
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = (ExtendedEntry)Element;
            KeybordNaxt(view);
        }

        private void SetBorder(ExtendedEntry view)
        {
            if (view != null)
            {
                if (view.HasBorder)
                {
                    Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                    Control.Layer.BorderWidth = (float)view.BorderWidth;
                    Control.Layer.CornerRadius = (float)view.CornerRadius;
                    Control.ClipsToBounds = true;
                }
                else
                {
                    Control.Layer.CornerRadius = 0f;
                }
                Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
                if (view.HasPin)
                {
                    Control.LeftView = new UIView(new CGRect(0, 0, 10, 0));
                    Control.LeftViewMode = UITextFieldViewMode.Always;
                }
            }
        }

        private void KeybordNaxt(ExtendedEntry view)
        {
            if (view != null)
            {
                if (view.ReturnNext)
                {
                    if (Control != null)
                    {
                        Control.KeyboardAppearance = UIKeyboardAppearance.Default;
                        if (view.ReturnKeyType == ReturnKeyTypes.Next)
                        {
                            Control.ReturnKeyType = UIReturnKeyType.Next;
                        }
                        else if (view.ReturnKeyType == ReturnKeyTypes.Done)
                        {
                            Control.ReturnKeyType = UIReturnKeyType.Done;
                        }
                    }
                }
            }
        }
    }

}