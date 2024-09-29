using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spectre.Console.Cli;

namespace Microsoft.Extensions.Hosting {

    /// <summary>
    /// Extensions for <see cref="IHostBuilder"/> and <see cref="HostApplicationBuilder"/>.
    /// </summary>
    public static class JaahasSpectreHostBuilderExtensions {

        /// <summary>
        /// Builds the host and runs the registered <see cref="CommandApp"/> with the specified arguments.
        /// </summary>
        /// <param name="builder">
        ///   The host builder.
        /// </param>
        /// <param name="args">
        ///   The command arguments.
        /// </param>
        /// <param name="cancellationToken">
        ///   The cancellation token to use when starting the host.
        /// </param>
        /// <returns>
        ///   The exit code of the command.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        public static async Task<int> BuildAndRunCommandAppAsync(this IHostBuilder builder, IEnumerable<string> args, CancellationToken cancellationToken = default) {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }
            if (args == null) {
                throw new ArgumentNullException(nameof(args));
            }

            using var host = builder.Build();
            await host.StartAsync(cancellationToken).ConfigureAwait(false);

            return await host.Services.RunSpectreCommandAppAsync(args).ConfigureAwait(false);
        }


        /// <summary>
        /// Builds the host and runs the registered <see cref="CommandApp{TDefaultCommand}"/> with the specified arguments.
        /// </summary>
        /// <typeparam name="TDefaultCommand">
        ///   The type of the default command.
        /// </typeparam>
        /// <param name="builder">
        ///   The host builder.
        /// </param>
        /// <param name="args">
        ///   The command arguments.
        /// </param>
        /// <param name="cancellationToken">
        ///   The cancellation token to use when starting the host.
        /// </param>
        /// <returns>
        ///   The exit code of the command.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        public static async Task<int> BuildAndRunCommandAppAsync<TDefaultCommand>(this IHostBuilder builder, IEnumerable<string> args, CancellationToken cancellationToken = default) where TDefaultCommand : class, ICommand {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }
            if (args == null) {
                throw new ArgumentNullException(nameof(args));
            }

            using var host = builder.Build();
            await host.StartAsync(cancellationToken).ConfigureAwait(false);

            return await host.Services.RunSpectreCommandAppAsync<TDefaultCommand>(args).ConfigureAwait(false);
        }


        /// <summary>
        /// Builds the host and runs the registered <see cref="CommandApp"/> with the specified arguments.
        /// </summary>
        /// <param name="builder">
        ///   The host application builder.
        /// </param>
        /// <param name="args">
        ///   The command arguments.
        /// </param>
        /// <param name="cancellationToken">
        ///   The cancellation token to use when starting the host.
        /// </param>
        /// <returns>
        ///   The exit code of the command.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        public static async Task<int> BuildAndRunCommandAppAsync(this HostApplicationBuilder builder, IEnumerable<string> args, CancellationToken cancellationToken = default) {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }
            if (args == null) {
                throw new ArgumentNullException(nameof(args));
            }

            using var host = builder.Build();
            await host.StartAsync(cancellationToken).ConfigureAwait(false);

            return await host.Services.RunSpectreCommandAppAsync(args).ConfigureAwait(false);
        }


        /// <summary>
        /// Builds the host and runs the registered <see cref="CommandApp{TDefaultCommand}"/> with the specified arguments.
        /// </summary>
        /// <typeparam name="TDefaultCommand">
        ///   The type of the default command.
        /// </typeparam>
        /// <param name="builder">
        ///   The host builder.
        /// </param>
        /// <param name="args">
        ///   The command arguments.
        /// </param>
        /// <param name="cancellationToken">
        ///   The cancellation token to use when starting the host.
        /// </param>
        /// <returns>
        ///   The exit code of the command.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        public static async Task<int> BuildAndRunCommandAppAsync<TDefaultCommand>(this HostApplicationBuilder builder, IEnumerable<string> args, CancellationToken cancellationToken = default) where TDefaultCommand : class, ICommand {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }
            if (args == null) {
                throw new ArgumentNullException(nameof(args));
            }

            using var host = builder.Build();
            await host.StartAsync(cancellationToken).ConfigureAwait(false);

            return await host.Services.RunSpectreCommandAppAsync<TDefaultCommand>(args).ConfigureAwait(false);
        }

    }
}
