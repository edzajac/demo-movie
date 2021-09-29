using Demo.Movie.Core.AppSetup;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using UIKit;

namespace Demo.Movie.iOS
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
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            string appCenterSecret = AppSettingsManager.Settings["iOSAppCenterSecret"];

            AppCenter.Start(appCenterSecret, typeof(Analytics), typeof(Crashes));

            Crashes.NotifyUserConfirmation(UserConfirmation.AlwaysSend);

            global::Xamarin.Forms.Forms.Init();

            Sharpnado.MaterialFrame.iOS.iOSMaterialFrameRenderer.Init();

            var initialize = new AppSetup.OnStart();

            LoadApplication(new App(initialize));

            return base.FinishedLaunching(app, options);
        }
    }
}
