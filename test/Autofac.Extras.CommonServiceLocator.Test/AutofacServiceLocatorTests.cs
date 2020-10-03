// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using Autofac.Extras.CommonServiceLocator.Test.Components;
using CommonServiceLocator;
using Xunit;

namespace Autofac.Extras.CommonServiceLocator.Test
{
    public sealed class AutofacServiceLocatorTests
    {
        private readonly IServiceLocator _locator;

        public AutofacServiceLocatorTests()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<SimpleLogger>()
                .Named<ILogger>(typeof(SimpleLogger).FullName!)
                .SingleInstance()
                .As<ILogger>();

            builder
                .RegisterType<AdvancedLogger>()
                .Named<ILogger>(typeof(AdvancedLogger).FullName!)
                .SingleInstance()
                .As<ILogger>();

            var container = builder.Build();

            _locator = new AutofacServiceLocator(container);
        }

        [Fact]
        public void Constructor_Does_Not_Accept_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new AutofacServiceLocator(null));
        }

        [Fact]
        public void AskingForInvalidComponentShouldRaiseActivationException()
        {
            Assert.Throws<ActivationException>(() => _locator.GetInstance<IDictionary>());
        }

        [Fact]
        public void GenericOverload_GetAllInstances()
        {
            var genericLoggers = new List<ILogger>(_locator.GetAllInstances<ILogger>());
            var plainLoggers = new List<object>(_locator.GetAllInstances(typeof(ILogger)));
            Assert.Equal(genericLoggers, plainLoggers);
        }

        [Fact]
        public void GenericOverload_GetInstance()
        {
            Assert.Equal(
                _locator.GetInstance<ILogger>().GetType(),
                _locator.GetInstance(typeof(ILogger), null).GetType());
        }

        [Fact]
        public void GenericOverload_GetInstance_WithName()
        {
            Assert.Equal(
                _locator.GetInstance<ILogger>(typeof(AdvancedLogger).FullName).GetType(),
                _locator.GetInstance(typeof(ILogger), typeof(AdvancedLogger).FullName).GetType());
        }

        [Fact]
        public void GetAllInstances()
        {
            var instances = _locator.GetAllInstances<ILogger>();
            IList<ILogger> list = new List<ILogger>(instances);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void GetInstance()
        {
            var instance = _locator.GetInstance<ILogger>();
            Assert.NotNull(instance);
        }

        [Fact]
        public void GetlAllInstance_ForUnknownType_ReturnEmptyEnumerable()
        {
            var instances = _locator.GetAllInstances<IDictionary>();
            IList<IDictionary> list = new List<IDictionary>(instances);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void GetNamedInstance()
        {
            var instance = _locator.GetInstance<ILogger>(typeof(AdvancedLogger).FullName);
            Assert.IsType<AdvancedLogger>(instance);
        }

        [Fact]
        public void GetNamedInstance_WithZeroLenName()
        {
            Assert.Throws<ActivationException>(() => _locator.GetInstance<ILogger>(""));
        }

        [Fact]
        public void GetNamedInstance2()
        {
            var instance = _locator.GetInstance<ILogger>(typeof(SimpleLogger).FullName);
            Assert.IsType<SimpleLogger>(instance);
        }

        [Fact]
        public void GetUnknownInstance2()
        {
            Assert.Throws<ActivationException>(() => _locator.GetInstance<ILogger>("test"));
        }

        [Fact]
        public void Overload_GetInstance_NoName_And_NullName()
        {
            Assert.Equal(
                _locator.GetInstance<ILogger>().GetType(),
                _locator.GetInstance<ILogger>(null).GetType());
        }
    }
}