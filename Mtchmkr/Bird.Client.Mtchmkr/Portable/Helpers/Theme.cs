using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xamarin.Forms;
using Bird.Client.Mtchmkr.Portable;

namespace Bird.Client.Mtchmkr.Helpers
{
    public static class TheTheme
    {
        public static void SetTheme()
        {
            switch (Settings.Theme)
            {
                //default
                case 0:
                    App.Current.UserAppTheme = OSAppTheme.Unspecified;
                    break;
                //light
                case 1:
                    App.Current.UserAppTheme = OSAppTheme.Light;
                    break;
                //dark
                case 2:
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
            }

            var nav = App.Current.MainPage as Xamarin.Forms.NavigationPage;

            var e = DependencyService.Get<IEnvironment>();
            if (App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                e?.SetStatusBarColor(System.Drawing.Color.Black, false);
                if (nav != null)
                {
                    nav.BarBackgroundColor = System.Drawing.Color.Black;
                    nav.BarTextColor = System.Drawing.Color.White;
                }
            }
            else
            {
                e?.SetStatusBarColor(System.Drawing.Color.White, true);
                if (nav != null)
                {
                    nav.BarBackgroundColor = System.Drawing.Color.White;
                    nav.BarTextColor = System.Drawing.Color.Black;
                }
            }


        }
    }
}
