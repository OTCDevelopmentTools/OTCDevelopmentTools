
using Bird.Client.Mtchmkr.Portable.Comon;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Helpers
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtention : IMarkupExtension
    {
        static Dictionary<Icons, string> m_Names = new Dictionary<Icons, string>();
        //    public Icons Source { get; set; } = Icons.None;
        public Icons Source { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Icons.None == Source)
            {
                return null;
            }

            if (!m_Names.ContainsKey(Source))
            {
                string source = string.Format("{0}.png", Source);
                string[] resourceNames = typeof(ImageResourceExtention).GetTypeInfo().Assembly.GetManifestResourceNames();
                foreach (string resourceName in resourceNames)
                {
                    if (resourceName.EndsWith(source) && !m_Names.ContainsKey(Source))
                    {
                        m_Names.Add(Source, resourceName);
                    }
                }
            }
            try
            {
                string name = m_Names[Source];
                System.Diagnostics.Debug.WriteLine("***************" + name + "****************************");
                var imageSource = ImageSource.FromResource(name, typeof(ImageResourceExtention).GetTypeInfo().Assembly);
                return imageSource;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
