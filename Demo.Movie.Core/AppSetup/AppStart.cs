
using Autofac;
using Demo.Movie.Core.Interfaces;
using Demo.Movie.Core.Services;
using Demo.Movie.Core.ViewModels;

namespace Demo.Movie.Core.AppSetup
{
    public class AppStart
    {
        public IContainer InitializeDependencies()
        {
            var builder = new ContainerBuilder();

            RegisterDependencies(builder);

            return builder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder builder)
        {
            // Services

            builder.RegisterType<MovieService>().As<IMovieService>().SingleInstance();

            // View Models

            builder.RegisterType<LandingPageViewModel>().UsingConstructor(typeof(IMovieService)).SingleInstance();
            builder.RegisterType<FilmModalViewModel>().UsingConstructor(typeof(IMovieService)).SingleInstance();
        }
    }

    public class AppContainer
    {
        public static IContainer Container { get; set; }
    }
}
