using System;
//using System.Drawing;
using System.Text;

namespace Bird.Client.Mtchmkr.Helpers
{
    public class Naming
    {
        public static string CreateInitials(string name)
        {
            StringBuilder builder = new StringBuilder();
            string[] names = name.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var _name in names)
            {
                builder.Append(_name.Substring(0, 1).ToUpper());
            }
            return builder.ToString();
        }
    }
}
