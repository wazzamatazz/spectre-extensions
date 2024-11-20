using System;

using Jaahas.Spectre.Extensions.Logging;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Logging {

    /// <summary>
    /// Extension methods for configuring logging using Spectre.Console.
    /// </summary>
    public static class JaahasSpectreLoggingBuilderExtensions {

        /// <summary>
        /// Adds a logger to the builder that writes to the console using Spectre.Console.
        /// </summary>
        /// <param name="builder">
        ///   The <see cref="ILoggingBuilder"/>.
        /// </param>
        /// <param name="configuration">
        ///   The <see cref="IConfiguration"/> to bind the <see cref="SpectreConsoleLoggerOptions"/> 
        ///   against.
        /// </param>
        /// <param name="configurationSection">
        ///   The name of the configuration section to bind the options against.
        /// </param>
        /// <returns>
        ///   The <see cref="ILoggingBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="configuration"/> is <see langword="null"/>.
        /// </exception>
        public static ILoggingBuilder AddSpectreConsole(this ILoggingBuilder builder, IConfiguration configuration, string? configurationSection = "SpectreConsoleLogger") {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }
            if (configuration == null) {
                throw new ArgumentNullException(nameof(configuration));
            }

            builder.AddSpectreConsole();
            builder.Services.Configure<SpectreConsoleLoggerOptions>(string.IsNullOrWhiteSpace(configurationSection)
                ? configuration
                : configuration.GetSection(configurationSection!));

            return builder;
        }

    }
}
