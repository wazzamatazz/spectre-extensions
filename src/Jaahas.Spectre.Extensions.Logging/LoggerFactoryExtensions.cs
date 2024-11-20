using System;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jaahas.Spectre.Extensions.Logging {

    /// <summary>
    /// Extension methods for configuring logging using Spectre.Console.
    /// </summary>
    public static class LoggerFactoryExtensions {

        /// <summary>
        /// Adds a logger to the factory that writes to the console using Spectre.Console.
        /// </summary>
        /// <param name="factory">
        ///   The <see cref="ILoggerFactory"/>.
        /// </param>
        /// <param name="options">
        ///   The options to use when creating the logger.
        /// </param>
        /// <returns>
        ///   The <see cref="ILoggerFactory"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="factory"/> is <see langword="null"/>.
        /// </exception>
        public static ILoggerFactory AddSpectreConsole(this ILoggerFactory factory, SpectreConsoleLoggerOptions? options = null) {
            if (factory == null) {
                throw new ArgumentNullException(nameof(factory));
            }

            factory.AddProvider(new SpectreConsoleLoggerProvider(options));
            return factory;
        }


        /// <summary>
        /// Adds a logger to the builder that writes to the console using Spectre.Console.
        /// </summary>
        /// <param name="builder">
        ///   The <see cref="ILoggingBuilder"/>.
        /// </param>
        /// <returns>
        ///   The <see cref="ILoggingBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        public static ILoggingBuilder AddSpectreConsole(this ILoggingBuilder builder) {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SpectreConsoleLoggerProvider>());
            return builder;
        }


        /// <summary>
        /// Adds a logger to the builder that writes to the console using Spectre.Console.
        /// </summary>
        /// <param name="builder">
        ///   The <see cref="ILoggingBuilder"/>.
        /// </param>
        /// <param name="configure">
        ///   A delegate that configures the <see cref="SpectreConsoleLoggerOptions"/> for the logger.
        /// </param>
        /// <returns>
        ///   The <see cref="ILoggingBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="configure"/> is <see langword="null"/>.
        /// </exception>
        public static ILoggingBuilder AddSpectreConsole(this ILoggingBuilder builder, Action<SpectreConsoleLoggerOptions> configure) {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }
            if (configure == null) {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddSpectreConsole();
            builder.Services.Configure(configure);

            return builder;
        }

    }
}
