﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.PushNotification;

namespace Bird.Client.Mtchmkr.Android
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();


            //Set the default notification channel for your app when running Android Oreo
            //if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            //{
            //    //Change for your default notification channel id here
            //    PushNotificationManager.DefaultNotificationChannelId = "DefaultChannel";

            //    //Change for your default notification channel name here
            //    PushNotificationManager.DefaultNotificationChannelName = "General";
            //}

            //If debug you should reset the token each time.
#if DEBUG
            PushNotificationManager.Initialize(this, true);
#else
            PushNotificationManager.Initialize(this, false);
#endif

            PushNotificationManager.IconResource = Resource.Drawable.Admin;

            //Handle notification when app is closed here
            CrossPushNotification.Current.OnNotificationReceived += (s, p) =>
            {

            };


        }
    }
}