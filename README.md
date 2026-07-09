# Autofac.Extras.CommonServiceLocator

Common Service Locator implementation for [Autofac](https://autofac.org).

[![Build status](https://github.com/autofac/Autofac.Extras.CommonServiceLocator/actions/workflows/main.yml/badge.svg)](https://github.com/autofac/Autofac.Extras.CommonServiceLocator/actions/workflows/main.yml) [![codecov](https://codecov.io/gh/Autofac/Autofac.Extras.CommonServiceLocator/branch/develop/graph/badge.svg)](https://codecov.io/gh/Autofac/Autofac.Extras.CommonServiceLocator) [![NuGet](https://img.shields.io/nuget/v/Autofac.Extras.CommonServiceLocator.svg)](https://nuget.org/packages/Autofac.Extras.CommonServiceLocator)

Please file issues and pull requests for this package in this repository rather than in the Autofac core repo.

- [Documentation](https://autofac.readthedocs.io/en/latest/integration/csl.html)
- [NuGet](https://www.nuget.org/packages/Autofac.Extras.CommonServiceLocator/)
- [Contributing](https://autofac.readthedocs.io/en/latest/contributors.html)
- [Open in Visual Studio Code](https://open.vscode.dev/autofac/Autofac.Extras.CommonServiceLocator)

## Quick Start

```csharp
var builder = new ContainerBuilder();

// Perform registrations and build the container.
var container = builder.Build();

// Set the service locator to an AutofacServiceLocator.
var csl = new AutofacServiceLocator(container);
ServiceLocator.SetLocatorProvider(() => csl);
```

Check out the [Autofac Common Service Locator documentation](https://autofac.readthedocs.io/en/latest/integration/csl.html) for more information.

## Get Help

**Need help with Autofac?** We have [a documentation site](https://autofac.readthedocs.io/) as well as [API documentation](https://autofac.org/apidoc/). We're ready to answer your questions on [Stack Overflow](https://stackoverflow.com/questions/tagged/autofac) or check out the [discussion forum](https://groups.google.com/forum/#forum/autofac).
