using Spectre.Console;
using Spectre.Console.Cli;

namespace ExampleCommandApp {
    internal class HelloCommand : Command<HelloCommand.Settings> {

        private readonly HelloService _helloService;

        private readonly ILogger<HelloCommand> _logger;


        public HelloCommand(HelloService helloService, ILogger<HelloCommand> logger) {
            _helloService = helloService;
            _logger = logger;
        }


        public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken) {
            AnsiConsole.WriteLine(_helloService.Greet(settings.Name));
            AnsiConsole.WriteLine();

            _logger.LogCritical("This is a critical message.");
            AnsiConsole.WriteLine();

            _logger.LogError("This is an error message.");
            AnsiConsole.WriteLine();

            _logger.LogError(new Exception("Example exception"), "This is an error message with an exception.");
            AnsiConsole.WriteLine();

            _logger.LogWarning("This is a warning message.");
            AnsiConsole.WriteLine();

            _logger.LogInformation("This is an information message.");
            AnsiConsole.WriteLine();

            _logger.LogDebug("This is a debug message.");
            AnsiConsole.WriteLine();

            _logger.LogTrace("This is a trace message.");
            AnsiConsole.WriteLine();

            return 0;
        }


        public class Settings : CommandSettings {

            [CommandArgument(0, "<NAME>")]
            public string Name { get; set; } = default!;

        }

    }
}
