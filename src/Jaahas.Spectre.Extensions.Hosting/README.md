# About

Jaahas.Spectre.Extensions.Hosting is a collection of extensions for [Spectre.Console.Cli](https://spectreconsole.net/cli/) to simplify integration of the Spectre `CommandApp` with Microsoft.Extensions.Hosting.


# Getting Started

You can register a `CommandApp` with your host builder as follows:

```csharp
using Spectre.Console.Cli;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSpectreCommandApp((IConfigurator options) => {
    // Configure your command app here.
});

return await builder.BuildAndRunCommandAppAsync(args);
```

You can use a `CommandApp<TDefaultCommand>` instead of a `CommandApp` by specifying the generic `TDefaultCommand` parameter when registering and running the command app:

```csharp
using Spectre.Console.Cli;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSpectreCommandApp<MyDefaultCommand>((IConfigurator options) => {
    // Configure your command app here.
});

return await builder.BuildAndRunCommandAppAsync<MyDefaultCommand>(args);
```

The configured command app is capable of resolving services that the `CommandApp` or `CommandApp<TDefaultCommand>` registers with the Spectre `ITypeRegistrar`, or that have been registered directly with the service collection. For example, you can inject services such as `ILogger<T>` into your command classes.

You can also register the command app directly with an `IServiceCollection` and run it directly against the `IServiceProvider` built from the service collection if preferred.


# Logging

You can register a logger that writes to the console using Spectre.Console's `AnsiConsole` class as follows:

```csharp
using Jaahas.Spectre.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders()
    .AddSpectreConsole();
```

> It is recommended that you clear existing providers when adding the Spectre console logger to avoid duplicate log entries from the default console logger.

You can configure the [colours and styles](https://spectreconsole.net/appendix/) used when writing log messages by calling the `AddSpectreConsole` overload that configures the registered `SpectreConsoleLoggerOptions`:

```csharp
using Jaahas.Spectre.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders()
    .AddSpectreConsole(options => {
        options.CriticalStyle = "bold magenta on green";
    });
```


