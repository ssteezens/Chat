using Chat.Models;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml;

namespace Chat.ViewModels
{
    public class AddChatRoomViewModel : ViewModelBase
	{
		private bool _isOpen;

		public AddChatRoomViewModel()
		{
			ChatRoomModel = new ChatRoom();
		}

		/// <summary>
        ///		Opens or closes the view.
        /// </summary>
		public bool IsOpen
		{
            get => _isOpen;
			set => Set(ref _isOpen, value, nameof(IsOpen));
		}
		
        public ChatRoom ChatRoomModel { get; set; }

		public void Submit(object sender, RoutedEventArgs e)
		{
			// todo: validation

			// todo: submit to server

            IsOpen = false;
		}
    }
}
