using Chat.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Chat.Controls
{
    /// <summary>
    ///		Control for adding a chat room.
    /// </summary>
    public sealed partial class AddChatRoomControl : UserControl
    {
		/// <summary>
        ///		Dependency property for view model.
        /// </summary>
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel",
				typeof(AddChatRoomViewModel), typeof(AddChatRoomControl), null);

		/// <summary>
        ///		View model for control.
        /// </summary>
		public AddChatRoomViewModel ViewModel
		{
			get => (AddChatRoomViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

        public AddChatRoomControl()
        {
            this.InitializeComponent();
        }
    }
}
