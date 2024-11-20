# About

Jaahas.Spectre.Extensions.Logging is a collection of extensions for [Spectre.Console.Cli](https://spectreconsole.net/cli/) to allow Microsoft.Extensions.Logging to write log messages to the console using Spectre.Console's `AnsiConsole` class.


# Getting Started

> If you are using Microsoft.Extensions.Hosting you can use the [Jaahas.Spectre.Extensions.Hosting](https://www.nuget.org/packages/Jaahas.Spectre.Extensions.Hosting) package instead of this package.

You can register a logger that writes to the console using Spectre.Console's `AnsiConsole` class as follows:

```csharp
var services = new ServiceCollection();

services.AddLogging(builder => {
    builder.ClearProviders().AddSpectreConsole();
});
```

> It is recommended that you clear existing providers when adding the Spectre console logger to avoid duplicate log entries from the default console logger.

You can configure the [colours and styles](https://spectreconsole.net/appendix/) used when writing log messages by calling the `AddSpectreConsole` overload that configures the registered `SpectreConsoleLoggerOptions`:

```csharp
var services = new ServiceCollection();

services.AddLogging(builder => {
    builder.ClearProviders().AddSpectreConsole(options => {
        options.CriticalStyle = "bold magenta on green";
    });
});
```


