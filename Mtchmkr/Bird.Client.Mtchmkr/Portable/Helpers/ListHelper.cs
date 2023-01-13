//using System.Drawing;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bird.Client.Mtchmkr.Helpers
{
    public static class ListHelper
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> list)
        {
            return new ObservableCollection<T>(list);
        }
    }
}
