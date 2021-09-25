
using Autofac;
using Demo.Movie.Core.AppSetup;

namespace Demo.Movie.iOS.AppSetup
{
    public class OnStart : AppStart
    {
        protected override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);

            // Additional Dependencies - iOS specific

        }
    }
}
