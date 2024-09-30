using System;

using Microsoft.Extensions.Logging;

using Spectre.Console;

namespace Jaahas.Spectre.Extensions.Logging {

    /// <summary>
    /// Formats and writes log messages to the console using <see cref="AnsiConsole"/>.
    /// </summary>
    internal class SpectreConsoleFormatter {

        /// <summary>
        /// Static lock to ensure that only one formatter writes to the console at a time.
        /// </summary>
        private static readonly object s_lock = new object();

        /// <summary>
        /// The formatter options.
        /// </summary>
        private readonly SpectreConsoleLoggerOptions _options;


        /// <summary>
        /// Creates a new <see cref="SpectreConsoleFormatter"/> with the specified options.
        /// </summary>
        /// <param name="options">
        ///   The formatter options.
        /// </param>
        public SpectreConsoleFormatter(SpectreConsoleLoggerOptions options) {
            _options = options;
        }


        /// <summary>
        /// Writes a log message to the console.
        /// </summary>
        /// <param name="logLevel">
        ///   The log level for the message.
        /// </param>
        /// <param name="message">
        ///   The message.
        /// </param>
        /// <param name="error">
        ///   The exception associated with the message, if any.
        /// </param>
        public void WriteMessage(LogLevel logLevel, string message, Exception? error) {
            var formattedMessage = logLevel switch {
                LogLevel.Critical => ApplyStyle(message, _options.CriticalStyle),
                LogLevel.Error => ApplyStyle(message, _options.ErrorStyle),
                LogLevel.Warning => ApplyStyle(message, _options.WarningStyle),
                LogLevel.Information => ApplyStyle(message, _options.InformationStyle),
                LogLevel.Debug => ApplyStyle(message, _options.DebugStyle),
                LogLevel.Trace => ApplyStyle(message, _options.TraceStyle),
                _ => $"[dim]{message}[/]"
            };

            lock (s_lock) {
                AnsiConsole.MarkupLine(formattedMessage);
                if (_options.WriteExceptions && error != null) {
                    AnsiConsole.WriteException(error, _options.ExceptionFormat);
                }
            }
        }


        /// <summary>
        /// Applies the specified style to the message.
        /// </summary>
        /// <param name="message">
        ///   The message.
        /// </param>
        /// <param name="style">
        ///   The style to apply.
        /// </param>
        /// <returns>
        ///   The message with the specified style applied.
        /// </returns>
        private static string ApplyStyle(string message, string? style) {
            return string.IsNullOrWhiteSpace(style)
                ? message
                : $"[{style}]{message}[/]";
        }

    }
}
