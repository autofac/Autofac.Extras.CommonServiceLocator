// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using Autofac.Extras.CommonServiceLocator.Test.Components;
using CommonServiceLocator;

namespace Autofac.Extras.CommonServiceLocator.Test;

public sealed class AutofacServiceLocatorTests
{
    [Fact]
    public void Ctor_NullComponentContext()
    {
        Assert.Throws<ArgumentNullException>(() => new AutofacServiceLocator(null!));
    }

    [Fact]
    public void GetAllInstances_GenericOverload()
    {
        var locator = CreateLocator();

        var genericLoggers = locator.GetAllInstances<ILogger>();
        var plainLoggers = locator.GetAllInstances(typeof(ILogger));
        Assert.Equal(genericLoggers, plainLoggers);
    }

    [Fact]
    public void GetAllInstances_NotRegistered()
    {
        var locator = CreateLocator();

        var instances = locator.GetAllInstances<IDictionary>();
        Assert.NotNull(instances);
        Assert.Empty(instances);
    }

    [Fact]
    public void GetAllInstances_Success()
    {
        var locator = CreateLocator();

        var instances = locator.GetAllInstances<ILogger>();
        Assert.Equal(2, instances.Count());
    }

    [Fact]
    public void GetInstance_NotRegistered()
    {
        var locator = CreateLocator();

        Assert.Throws<ActivationException>(() => locator.GetInstance<IDictionary>());
    }

    [Fact]
    public void GetInstance_GenericOverload()
    {
        var locator = CreateLocator();

        Assert.Equal(
            locator.GetInstance<ILogger>().GetType(),
            locator.GetInstance(typeof(ILogger), null).GetType());
    }

    [Fact]
    public void GetInstance_GenericOverloadWithName()
    {
        var locator = CreateLocator();

        Assert.Equal(
            locator.GetInstance<ILogger>(typeof(AdvancedLogger).FullName).GetType(),
            locator.GetInstance(typeof(ILogger), typeof(AdvancedLogger).FullName).GetType());
    }

    [Fact]
    public void GetInstance_EmptyName()
    {
        var locator = CreateLocator();

        Assert.Throws<ActivationException>(() => locator.GetInstance<ILogger>(""));
    }

    [Fact]
    public void GetInstance_NamedInstance()
    {
        var locator = CreateLocator();

        var instance = locator.GetInstance<ILogger>(typeof(AdvancedLogger).FullName);
        Assert.IsType<AdvancedLogger>(instance);
    }

    [Fact]
    public void GetInstance_NullName()
    {
        var locator = CreateLocator();

        Assert.Equal(
            locator.GetInstance<ILogger>().GetType(),
            locator.GetInstance<ILogger>(null).GetType());
    }

    [Fact]
    public void GetInstance_TypedInstance()
    {
        var locator = CreateLocator();

        var instance = locator.GetInstance<ILogger>();
        Assert.NotNull(instance);
    }

    [Fact]
    public void GetInstance_UnknownName()
    {
        var locator = CreateLocator();

        Assert.Throws<ActivationException>(() => locator.GetInstance<ILogger>("test"));
    }

    private static AutofacServiceLocator CreateLocator()
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

        return new AutofacServiceLocator(container);
    }
}
