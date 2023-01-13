using System.Collections.Generic;
//using System.Drawing;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Helpers
{
    public interface IEnvironment
    {
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
    }
}
