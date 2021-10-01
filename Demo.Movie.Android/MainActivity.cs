
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Demo.Movie.Core.AppSetup;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Demo.Movie.Droid
{
    [Activity(Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string appCenterSecret = AppSettingsManager.Settings["AndroidAppCenterSecret"];

            AppCenter.Start(appCenterSecret, typeof(Analytics), typeof(Crashes));

            Crashes.NotifyUserConfirmation(UserConfirmation.AlwaysSend);

            Sharpnado.HorizontalListView.Droid.SharpnadoInitializer.Initialize();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var initialize = new AppSetup.OnStart();

            LoadApplication(new App(initialize));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}