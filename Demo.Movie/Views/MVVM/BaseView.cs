using System.Threading;
using Autofac;
using Demo.Movie.Core.AppSetup;
using Demo.Movie.Core.ViewModels.MVVM;
using Xamarin.Forms;

namespace Demo.Movie.Views.MVVM
{
    public class BaseView<T> : ContentPage where T : BaseViewModel
    {
        private long _initialized;

        private readonly T _viewModel;

        public T ViewModel
        {
            get => _viewModel;
        }

        /// <summary>
        /// Initializes a new instance of the BaseView class.
        /// </summary>
        public BaseView()
        {
            BackgroundColor = Color.Black;

            _viewModel = AppContainer.Container.Resolve<T>();

            BindingContext = _viewModel;
        }

        /// <summary>
        /// Overrides the OnAppearing method and calls the ViewModel's InitAsync method.
        /// </summary>
        protected override async void OnAppearing()
        {
            if (Interlocked.CompareExchange(ref _initialized, 1, 0) == 0)
            {
                await ViewModel.InitAsync();
            }

            base.OnAppearing();
        }
    }
}
