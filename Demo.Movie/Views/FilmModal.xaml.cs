using System;
using Demo.Movie.Core.Model;
using Demo.Movie.Core.ViewModels;
using Demo.Movie.Views.MVVM;

namespace Demo.Movie.Views
{
    public partial class FilmModal : BaseView<FilmModalViewModel>
    {
        public FilmModal(Film film)
        {
            ViewModel.ChosenFilm = film;

            InitializeComponent();
        }

        /// <summary>
        /// Pops the Film Modal from the Navigation Stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BackButton_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
