using Demo.Movie.Views;
using Xamarin.Forms;

namespace Demo.Movie
{
    public partial class App : Application
    {
        public App(Core.AppSetup.AppStart onStart)
        {
            Core.Helpers.AkavacheCache.InitCache();

            Core.AppSetup.AppContainer.Container = onStart.InitializeDependencies();

            InitializeComponent();

            Sharpnado.MaterialFrame.Initializer.Initialize(false, false); 

            MainPage = new NavigationPage(new LandingPage());
        }

        protected override void OnStart() { }

        protected override void OnResume() { }

        protected override void OnSleep()
        {
            Core.Helpers.AkavacheCache.EnsureCaches();
        }
    }
}
