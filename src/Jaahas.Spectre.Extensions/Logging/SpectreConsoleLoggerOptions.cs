using Spectre.Console;

namespace Jaahas.Spectre.Extensions.Logging {

    /// <summary>
    /// Options for a Spectre console logger.
    /// </summary>
    public class SpectreConsoleLoggerOptions {

        /// <summary>
        /// The style to use for critical log messages.
        /// </summary>
        public string? CriticalStyle { get; set; } = "bold white on red";

        /// <summary>
        /// The style to use for error log messages.
        /// </summary>
        public string? ErrorStyle { get; set; } = "bold red";

        /// <summary>
        /// The style to use for warning log messages.
        /// </summary>
        public string? WarningStyle { get; set; } = "bold yellow";

        /// <summary>
        /// The style to use for information log messages.
        /// </summary>
        public string? InformationStyle { get; set; } = null;

        /// <summary>
        /// The style to use for debug log messages.
        /// </summary>
        public string? DebugStyle { get; set; } = "dim";

        /// <summary>
        /// The style to use for trace log messages.
        /// </summary>
        public string? TraceStyle { get; set; } = "dim";

        /// <summary>
        /// Specifies if exceptions should be written to the console.
        /// </summary>
        public bool WriteExceptions { get; set; } = true;

        /// <summary>
        /// The format to use when writing exceptions to the console.
        /// </summary>
        public ExceptionFormats ExceptionFormat { get; set; } = ExceptionFormats.Default;

    }
}
