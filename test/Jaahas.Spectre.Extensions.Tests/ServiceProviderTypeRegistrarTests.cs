using Microsoft.Extensions.DependencyInjection;

using Spectre.Console.Cli;

using SpectreConsole = Spectre.Console;

namespace Jaahas.Spectre.Extensions.Tests {

    [TestClass]
    public class ServiceProviderTypeRegistrarTests {

        [TestMethod]
        public void ShouldPassTypeRegistrarTests() {
            var provider = new ServiceCollection()
                .BuildServiceProvider();

            var testHarness = new SpectreConsole.Testing.TypeRegistrarBaseTests(() => new ServiceProviderTypeRegistrar(provider));
            testHarness.RunAllTests();
        }

    }

}
