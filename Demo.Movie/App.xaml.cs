using Demo.Movie.Core.AppSetup;
using Demo.Movie.Core.Helpers;
using Demo.Movie.Views;
using Xamarin.Forms;

namespace Demo.Movie
{
    public partial class App : Application
    {
        public App(AppStart onStart)
        {
            AkavacheCache.InitCache();

            AppContainer.Container = onStart.InitializeDependencies();

            InitializeComponent();

            MainPage = new NavigationPage(new LandingPage());
        }

        protected override void OnStart() { }

        protected override void OnResume() { }

        protected override void OnSleep()
        {
            AkavacheCache.EnsureCaches();
        }
    }
}
