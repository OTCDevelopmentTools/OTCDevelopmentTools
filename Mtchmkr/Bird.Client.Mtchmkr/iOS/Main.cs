using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Bird.Client.Mtchmkr.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            try
            {
                // if you want to use a different Application Delegate class from "AppDelegate"
                // you can specify it here.
                //  UIApplication.Main(args, null, "AppDelegate");
                UIApplication.Main(args, null, typeof(AppDelegate));
            }
            catch (Exception ex)
            {

            }
      
        }
    }
}
