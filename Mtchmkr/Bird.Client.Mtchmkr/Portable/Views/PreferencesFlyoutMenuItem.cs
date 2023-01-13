using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bird.Client.Mtchmkr.Portable.Views
{
    public class PreferencesFlyoutMenuItem
    {
        public PreferencesFlyoutMenuItem()
        {
            TargetType = typeof(PreferencesFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}