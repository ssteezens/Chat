using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatWpf.Helpers;

namespace ChatWpf.Controls
{
    /// <summary>
    ///     Interaction logic for CenteredPopupControl.xaml
    /// </summary>
    [ContentProperty(nameof(InnerContent))]
    public partial class CenteredPopupControl : UserControl
    {
        /// <summary>
        ///     Dependency property for the control's inner content.
        /// </summary>
        public static readonly DependencyProperty InnerContentProperty = DependencyProperty.Register(
            "InnerContent", typeof(FrameworkElement), typeof(CenteredPopupControl), new PropertyMetadata(default(FrameworkElement)));

        /// <summary>
        ///     Dependency property for the popup's IsOpen property.
        /// </summary>
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            "IsOpen", typeof(bool), typeof(CenteredPopupControl), new PropertyMetadata(default(bool)));

        /// <summary>
        ///     Gets or sets whether the control's popup is open.
        /// </summary>
        public bool IsOpen
        {
            get => (bool) GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        /// <summary>
        ///     Gets or sets the control's inner content.
        /// </summary>
        public FrameworkElement InnerContent
        {
            get => (FrameworkElement) GetValue(InnerContentProperty);
            set => SetValue(InnerContentProperty, value);
        }

        public CenteredPopupControl()
        {
            InitializeComponent();

            var window = ControlInstances.MainWindow;

            CenteredPopup.PlacementTarget = window;

            // show/hide window overlay
            CenteredPopup.Opened += (sender, args) =>
            {
                var overlay = ControlInstances.WindowOverlay;

                overlay.Visibility = Visibility.Visible;
            };
            CenteredPopup.Closed += (sender, args) =>
            {
                var overlay = ControlInstances.WindowOverlay;

                overlay.Visibility = Visibility.Hidden;
            };

            // hack to reset the popup's positioning.
            window.LocationChanged += (sender, args) =>
            {
                var offset = CenteredPopup.HorizontalOffset;
                CenteredPopup.HorizontalOffset = offset + 1;
                CenteredPopup.HorizontalOffset = offset;
            };

            window.SizeChanged += (sender, args) =>
            {
                var offset = CenteredPopup.HorizontalOffset;
                CenteredPopup.HorizontalOffset = offset + 1;
                CenteredPopup.HorizontalOffset = offset;

                CenteredPopup.Width = window.Width;
                CenteredPopup.Height = window.Height;
            };
        }
    }
}
