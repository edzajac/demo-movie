
using Demo.Movie.Core.Model;
using Demo.Movie.Core.ViewModels;
using Demo.Movie.Views.MVVM;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Demo.Movie.Views
{
    public partial class FilmModal : BaseView<FilmModalViewModel>
    {
        public FilmModal(Film film)
        {
            ViewModel.ChosenFilm = film;

            On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.OverFullScreen);

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.PopFilmModalAction = PopFilmModal;
        }

        protected override void OnDisappearing()
        {
            ViewModel.PopFilmModalAction = null;

            base.OnDisappearing();
        }

        /// <summary>
        /// Pops the Film Modal from the Navigation Stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void PopFilmModal()
        {
            await Navigation.PopModalAsync();
        }
    }
}
