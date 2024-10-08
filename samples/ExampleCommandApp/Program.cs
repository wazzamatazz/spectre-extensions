using ExampleCommandApp;

using Jaahas.Spectre.Extensions.Logging;

using Spectre.Console.Cli;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders()
    .AddSpectreConsole(builder.Configuration);

builder.Services.AddSpectreCommandApp<HelloCommand>(options => {
    options.SetApplicationName("HelloCommandApp");
});

builder.Services.AddTransient<HelloService>();

// Normally you would pass in args here but for the sake of the example we'll just use the current
// user name.
return await builder.BuildAndRunCommandAppAsync<HelloCommand>([Environment.UserName]);
