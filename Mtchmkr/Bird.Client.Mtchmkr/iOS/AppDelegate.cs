using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using iZettle;
using Plugin.GoogleClient;
using Plugin.PushNotification;
using UIKit;

namespace Bird.Client.Mtchmkr.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        const string clientId = @"f0f40ba2-0904-4d79-b723-22e558a253e8";

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            iZettleSDK.Shared.StartWithAPIKey(clientId);
            global::Xamarin.Forms.Forms.Init();
            ImageCircleRenderer.Init();
            GoogleClientManager.Initialize();
            LoadApplication(new Bird.Client.Mtchmkr.Portable.App());

            PushNotificationManager.Initialize(options, true);

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return GoogleClientManager.OnOpenUrl(app, url, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            PushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            PushNotificationManager.RemoteNotificationRegistrationFailed(error);

        }
        // To receive notifications in foregroung on iOS 9 and below.
        // To receive notifications in background in any iOS version
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {

            PushNotificationManager.DidReceiveMessage(userInfo);
        }
    }
}
