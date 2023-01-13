using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Bird.Client.Mtchmkr.Helpers
{
    public static class Settings
    {
        // 0 = default, 1 = light, 2 = dark
        const int theme = 2;
        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }
    }
}
