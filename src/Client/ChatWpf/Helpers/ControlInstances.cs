using System.Windows.Controls;

namespace ChatWpf.Helpers
{
    /// <summary>
    ///     Helper class for accessing certain static controls.
    /// </summary>
    public static class ControlInstances
    {
        /// <summary>
        ///     The application's main window.
        /// </summary>
        public static MainWindow MainWindow { get; set; }

        /// <summary>
        ///     The application's window overlay.
        /// </summary>
        public static Border WindowOverlay { get; set; }
    }
}
