using ExampleCommandApp;

using Spectre.Console.Cli;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSpectreCommandApp(options => {
    options.SetApplicationName("HelloCommandApp");
    options.AddCommand<HelloCommand>("hello");
});

builder.Services.AddTransient<HelloService>();

// Normally you would pass in args here but for the sake of the example we'll just use the current
// user name.
return await builder.BuildAndRunCommandAppAsync<HelloCommand>([Environment.UserName]);
