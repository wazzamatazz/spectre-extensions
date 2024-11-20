using System;

using Microsoft.Extensions.Logging;

using Spectre.Console;

namespace Jaahas.Spectre.Extensions.Logging {

    /// <summary>
    /// <see cref="ILogger"/> that writes to the console using <see cref="AnsiConsole"/>.
    /// </summary>
    internal sealed class SpectreConsoleLogger : ILogger {

        /// <summary>
        /// The formatter to use when writing log messages.
        /// </summary>
        internal SpectreConsoleFormatter Formatter { get; set; } = default!;


        /// <summary>
        /// Creates a new <see cref="SpectreConsoleLogger"/> with the specified formatter.
        /// </summary>
        /// <param name="formatter">
        ///   The formatter to use when writing log messages.
        /// </param>
        public SpectreConsoleLogger(SpectreConsoleFormatter formatter) {
            Formatter = formatter;
        }


        /// <inheritdoc/>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
            if (!IsEnabled(logLevel)) {
                return;
            }

            // Do not pass the exception here; we'll write it below using AnsiConsole.WriteException
            // if it is non-null.
            var message = formatter(state, null);
            Formatter.WriteMessage(logLevel, message, exception);
        }


        /// <inheritdoc/>
        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None && (!Formatter.MinimumLogLevel.HasValue || logLevel >= Formatter.MinimumLogLevel.Value);


        /// <inheritdoc/>
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    }
}
