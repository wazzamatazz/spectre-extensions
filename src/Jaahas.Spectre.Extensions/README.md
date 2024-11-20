# About

Jaahas.Spectre.Extensions is a collection of extensions for [Spectre.Console.Cli](https://spectreconsole.net/cli/) to simplify integration of the Spectre `CommandApp` with Microsoft.Extensions.DependencyInjection.


# Getting Started

> If you are using Microsoft.Extensions.Hosting you can use the [Jaahas.Spectre.Extensions.Hosting](https://www.nuget.org/packages/Jaahas.Spectre.Extensions.Hosting) package instead of this package.

You can register a `CommandApp` with your host builder as follows:

```csharp
var services = new ServiceCollection();

services.AddSpectreCommandApp((IConfigurator options) => {
    // Configure your command app here.
});

var provider = services.BuildServiceProvider();

return await provider.RunSpectreCommandAppAsync(args);
```

You can use a `CommandApp<TDefaultCommand>` instead of a `CommandApp` by specifying the generic `TDefaultCommand` parameter when registering and running the command app:

```csharp
var services = new ServiceCollection();

services.AddSpectreCommandApp<MyDefaultCommand>((IConfigurator options) => {
    // Configure your command app here.
});

var provider = services.BuildServiceProvider();

return await provider.RunSpectreCommandAppAsync<MyDefaultCommand>(args);
```

The configured command app is capable of resolving services that the `CommandApp` or `CommandApp<TDefaultCommand>` registers with the Spectre `ITypeRegistrar`, or that have been registered directly with the service collection. For example, you can inject services such as `ILogger<T>` into your command classes.
