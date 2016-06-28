using System;
using Autofac.Extras.CommonServiceLocator.Test.Components;
using CommonServiceLocator.AutofacAdapter;
using Microsoft.Practices.ServiceLocation;
using Xunit;

namespace Autofac.Extras.CommonServiceLocator.Test
{
    public sealed class AutofacServiceLocatorTests : ServiceLocatorTestCase
    {
        [Fact]
        public void Constructor_Does_Not_Accept_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new AutofacServiceLocator(null));
        }

        protected override IServiceLocator CreateServiceLocator()
        {
            return new AutofacServiceLocator(CreateContainer());
        }

        private static IComponentContext CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<SimpleLogger>()
                .Named<ILogger>(typeof(SimpleLogger).FullName)
                .SingleInstance()
                .As<ILogger>();

            builder
                .RegisterType<AdvancedLogger>()
                .Named<ILogger>(typeof(AdvancedLogger).FullName)
                .SingleInstance()
                .As<ILogger>();

            return builder.Build();
        }
    }
}