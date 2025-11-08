using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Spectre.Console.Cli;

namespace System {

    /// <summary>
    /// Spectre.Console.Cli extensions for <see cref="IServiceProvider"/>.
    /// </summary>
    public static class JaahasSpectreServiceProviderExtensions {

        /// <summary>
        /// Runs the registered <see cref="CommandApp"/> with the specified arguments.
        /// </summary>
        /// <param name="provider">
        ///   The service provider.
        /// </param>
        /// <param name="args">
        ///   The command arguments.
        /// </param>
        /// <param name="cancellationToken">
        ///   The cancellation token to pass to the command app.
        /// </param>
        /// <returns>
        ///   The exit code of the command app.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="provider"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        public static async Task<int> RunSpectreCommandAppAsync(this IServiceProvider provider, IEnumerable<string> args, CancellationToken cancellationToken = default) {
            if (provider == null) {
                throw new ArgumentNullException(nameof(provider));
            }
            if (args == null) {
                throw new ArgumentNullException(nameof(args));
            }

            using var scope = provider.CreateScope();
            var commandApp = scope.ServiceProvider.GetRequiredService<CommandApp>();

            return await commandApp.RunAsync(args, cancellationToken).ConfigureAwait(false);
        }


        /// <summary>
        /// Runs the registered <see cref="CommandApp{TDefaultCommand}"/> with the specified arguments.
        /// </summary>
        /// <typeparam name="TDefaultCommand">
        ///   The type of the default command.
        /// </typeparam>
        /// <param name="provider">
        ///   The service provider.
        /// </param>
        /// <param name="args">
        ///   The command arguments.
        /// </param>
        /// <param name="cancellationToken">
        ///   The cancellation token to pass to the command app.
        /// </param>
        /// <returns>
        ///   The exit code of the command app.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="provider"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        public static async Task<int> RunSpectreCommandAppAsync<TDefaultCommand>(this IServiceProvider provider, IEnumerable<string> args, CancellationToken cancellationToken = default) where TDefaultCommand : class, ICommand {
            if (provider == null) {
                throw new ArgumentNullException(nameof(provider));
            }
            if (args == null) {
                throw new ArgumentNullException(nameof(args));
            }

            using var scope = provider.CreateScope();
            var commandApp = scope.ServiceProvider.GetRequiredService<CommandApp<TDefaultCommand>>();

            return await commandApp.RunAsync(args, cancellationToken).ConfigureAwait(false);
        }

    }
}
