using System.Collections;
using System.Collections.Generic;
using Autofac.Extras.CommonServiceLocator.Test.Components;
using Microsoft.Practices.ServiceLocation;
using Xunit;

namespace CommonServiceLocator.AutofacAdapter
{
    public abstract class ServiceLocatorTestCase
    {
        private IServiceLocator _locator;

        public ServiceLocatorTestCase()
        {
            this._locator = this.CreateServiceLocator();
        }

        [Fact]
        public void AskingForInvalidComponentShouldRaiseActivationException()
        {
            Assert.Throws<ActivationException>(() => this._locator.GetInstance<IDictionary>());
        }

        [Fact]
        public void GenericOverload_GetAllInstances()
        {
            var genericLoggers = new List<ILogger>(this._locator.GetAllInstances<ILogger>());
            var plainLoggers = new List<object>(this._locator.GetAllInstances(typeof(ILogger)));
            Assert.Equal(genericLoggers, plainLoggers);
        }

        [Fact]
        public void GenericOverload_GetInstance()
        {
            Assert.Equal(
this._locator.GetInstance<ILogger>().GetType(),
this._locator.GetInstance(typeof(ILogger), null).GetType());
        }

        [Fact]
        public void GenericOverload_GetInstance_WithName()
        {
            Assert.Equal(
this._locator.GetInstance<ILogger>(typeof(AdvancedLogger).FullName).GetType(),
this._locator.GetInstance(typeof(ILogger), typeof(AdvancedLogger).FullName).GetType());
        }

        [Fact]
        public void GetAllInstances()
        {
            var instances = this._locator.GetAllInstances<ILogger>();
            IList<ILogger> list = new List<ILogger>(instances);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void GetInstance()
        {
            var instance = this._locator.GetInstance<ILogger>();
            Assert.NotNull(instance);
        }

        [Fact]
        public void GetlAllInstance_ForUnknownType_ReturnEmptyEnumerable()
        {
            var instances = this._locator.GetAllInstances<IDictionary>();
            IList<IDictionary> list = new List<IDictionary>(instances);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void GetNamedInstance()
        {
            var instance = this._locator.GetInstance<ILogger>(typeof(AdvancedLogger).FullName);
            Assert.IsType<AdvancedLogger>(instance);
        }

        [Fact]
        public void GetNamedInstance_WithZeroLenName()
        {
            Assert.Throws<ActivationException>(() => this._locator.GetInstance<ILogger>(""));
        }

        [Fact]
        public void GetNamedInstance2()
        {
            var instance = this._locator.GetInstance<ILogger>(typeof(SimpleLogger).FullName);
            Assert.IsType<SimpleLogger>(instance);
        }

        [Fact]
        public void GetUnknownInstance2()
        {
            Assert.Throws<ActivationException>(() => this._locator.GetInstance<ILogger>("test"));
        }

        [Fact]
        public void Overload_GetInstance_NoName_And_NullName()
        {
            Assert.Equal(
this._locator.GetInstance<ILogger>().GetType(),
this._locator.GetInstance<ILogger>(null).GetType());
        }

        protected abstract IServiceLocator CreateServiceLocator();
    }
}