using System;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using SpectreConsole = Spectre.Console;

namespace Jaahas.Spectre.Extensions.Logging {

    /// <summary>
    /// <see cref="ILoggerProvider"/> that creates loggers that write to the console using 
    /// <see cref="SpectreConsole.AnsiConsole"/>.
    /// </summary>
    internal sealed class SpectreConsoleLoggerProvider : ILoggerProvider {

        /// <summary>
        /// Specifies if the provider has been disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// The provider options, if the provider was created using <see cref="SpectreConsoleLoggerProvider(SpectreConsoleLoggerOptions?)"/>.
        /// </summary>
        private readonly SpectreConsoleLoggerOptions? _options;

        /// <summary>
        /// The options monitor, if the provider was created using <see cref="SpectreConsoleLoggerProvider(IOptionsMonitor{SpectreConsoleLoggerOptions})"/>.
        /// </summary>
        private readonly IOptionsMonitor<SpectreConsoleLoggerOptions>? _optionsMonitor;

        /// <summary>
        /// The subscription to <see cref="_optionsMonitor"/>.
        /// </summary>
        private readonly IDisposable? _optionsMonitorSubscription;

        /// <summary>
        /// The logger instance.
        /// </summary>
        private SpectreConsoleLogger? _instance;


        /// <summary>
        /// Creates a new <see cref="SpectreConsoleLoggerProvider"/> with the specified options.
        /// </summary>
        /// <param name="optionsMonitor">
        ///   The options monitor that provides the logger options.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="optionsMonitor"/> is <see langword="null"/>.
        /// </exception>
        public SpectreConsoleLoggerProvider(IOptionsMonitor<SpectreConsoleLoggerOptions> optionsMonitor) {
            _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
            _optionsMonitorSubscription = _optionsMonitor.OnChange(ReloadOptions);
        }


        /// <summary>
        /// Creates a new <see cref="SpectreConsoleLoggerProvider"/> with the specified options.
        /// </summary>
        /// <param name="options">
        ///   The options to use when creating the logger.
        /// </param>
        internal SpectreConsoleLoggerProvider(SpectreConsoleLoggerOptions? options) {
            _options = options ?? new SpectreConsoleLoggerOptions();
        }


        /// <summary>
        /// Handles changes to the logger options.
        /// </summary>
        /// <param name="options">
        ///   The updated logger options.
        /// </param>
        /// <param name="name">
        ///   Not used.
        /// </param>
        private void ReloadOptions(SpectreConsoleLoggerOptions options, string? name) {
            if (_instance == null) {
                return;
            }

            _instance.Formatter = new SpectreConsoleFormatter(options);
        }


        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName) { 
            if (_disposed) {
                throw new ObjectDisposedException(GetType().FullName);
            }

            _instance ??= new SpectreConsoleLogger(new SpectreConsoleFormatter(_options ?? _optionsMonitor!.CurrentValue));
            return _instance;
        }


        /// <inheritdoc/>
        public void Dispose() {
            if (_disposed) {
                return;
            }

            _optionsMonitorSubscription?.Dispose();
            _disposed = true;
        }

    }
}
