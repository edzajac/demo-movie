using System;
using System.Threading.Tasks;
using Demo.Movie.Core.Model;
using Demo.Movie.Core.ViewModels;
using Demo.Movie.Views.MVVM;
using Xamarin.Forms;

namespace Demo.Movie.Views
{
    public partial class LandingPage : BaseView<LandingPageViewModel>
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.NavigateToFilmModalAction = NavigateToFilmModal;
        }

        protected override void OnDisappearing()
        {
            ViewModel.NavigateToFilmModalAction = null;

            base.OnDisappearing();
        }

        /// <summary>
        /// Prevents the android implmentation from
        /// displaying color when an item in 
        /// the GenreListView is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GenreListView_ItemSelected(Object sender, SelectedItemChangedEventArgs e)
        {
            if(Xamarin.Essentials.DeviceInfo.Platform == Xamarin.Essentials.DevicePlatform.Android)
            {
                GenreListView.SelectedItem = null;
            }
        }

        public async Task OnReveal_FlipAnimation(ViewCell viewCell)
        {

            await viewCell.View.FadeTo(1);

            await viewCell.View.RotateYTo(0);
        }

        /// <summary>
        /// Pushes the FilmModal onto the Navigation stack
        /// </summary>
        /// <param name="film"></param>
        public async void NavigateToFilmModal(Film film)
        {
            var tst = new FilmModal(film);

            await Navigation.PushModalAsync(new FilmModal(film));
        }
    }
}
