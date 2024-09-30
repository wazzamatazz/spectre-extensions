# About

Jaahas.Spectre.Extensions is a collection of extensions for [Spectre.Console.Cli](https://spectreconsole.net/cli/) to simplify integration of the Spectre `CommandApp` with Microsoft.Extensions.DependencyInjection.


# Registering and Running a CommandApp

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



# Building the Solution

The repository uses [Cake](https://cakebuild.net/) for cross-platform build automation. The build script allows for metadata such as a build counter to be specified when called by a continuous integration system such as TeamCity.

A build can be run from the command line using the [build.ps1](/build.ps1) PowerShell script or the [build.sh](/build.sh) Bash script. For documentation about the available build script parameters, see [build.cake](/build.cake).


# Software Bill of Materials

To generate a Software Bill of Materials (SBOM) for the repository in [CycloneDX](https://cyclonedx.org/) XML format, run [build.ps1](./build.ps1) or [build.sh](./build.sh) with the `--target BillOfMaterials` parameter.

The resulting SBOM is written to the `artifacts/bom` folder.
