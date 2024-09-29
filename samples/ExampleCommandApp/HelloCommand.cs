using Spectre.Console;
using Spectre.Console.Cli;

namespace ExampleCommandApp {
    internal class HelloCommand : Command<HelloCommand.Settings> {

        private readonly HelloService _helloService;


        public HelloCommand(HelloService helloService) {
            _helloService = helloService;
        }


        public override int Execute(CommandContext context, Settings settings) {
            AnsiConsole.WriteLine(_helloService.Greet(settings.Name));
            return 0;
        }


        public class Settings : CommandSettings {

            [CommandArgument(0, "<NAME>")]
            public string Name { get; set; } = default!;

        }

    }
}
