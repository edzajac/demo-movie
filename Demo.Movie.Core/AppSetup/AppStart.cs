
using Autofac;
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


            // View Models

            builder.RegisterType<LandingPageViewModel>().SingleInstance();
        }
    }

    public class AppContainer
    {
        public static IContainer Container { get; set; }
    }
}
