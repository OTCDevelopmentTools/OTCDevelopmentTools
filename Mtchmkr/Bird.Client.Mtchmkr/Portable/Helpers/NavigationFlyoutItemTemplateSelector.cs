//using System.Drawing;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.Helpers
{
    public class NavigationFlyoutItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NavigationHeaderTemplate { get; set; }
        public DataTemplate NavigationItemTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //Returning null, because at this point I'm not sure how to select the correct template
            return null;
        }
    }
}
