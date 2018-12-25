using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Chat.Controls.Common
{
	/// <summary>
    ///		Control for displaying a circular icon for things such as profile
    ///		pictures.
    /// </summary>
    public sealed partial class CircleIconControl : UserControl
    {
		public static readonly DependencyProperty IconImageProperty = 
			DependencyProperty.Register("IconImage",
			typeof(ImageSource), typeof(CircleIconControl), null);

		/// <summary>
        ///		Filename path for icon image.
        /// </summary>
		public ImageSource IconImage
		{
			get => (ImageSource) GetValue(IconImageProperty);
			set => SetValue(IconImageProperty, value);
		}

        public CircleIconControl()
        {
            this.InitializeComponent();
        }
    }
}
