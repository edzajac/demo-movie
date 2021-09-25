using Demo.Movie.Core.AppSetup;
using Demo.Movie.Views;
using Xamarin.Forms;

namespace Demo.Movie
{
    public partial class App : Application
    {
        public App(AppStart onStart)
        {
            AppContainer.Container = onStart.InitializeDependencies();

            InitializeComponent();

            MainPage = new LandingPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
