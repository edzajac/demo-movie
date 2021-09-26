using System;
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

        public void GenreListView_ItemSelected(Object sender, SelectedItemChangedEventArgs e)
        {
            GenreListView.SelectedItem = null;
        }
    }
}
