using System.Collections.Generic;
using Chat.Models;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Messaging;

namespace Chat.ViewModels
{
    public class AddChatRoomViewModel : ViewModelBase
	{
		private bool _isOpen;

		public AddChatRoomViewModel()
		{
			ChatRoomModel = new ChatRoom()
			{
				Users = new List<User>() { UserInstance.Current },
				ChatMessages = new List<ChatMessage>()
			};
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

			// send event to be handled on main view model
			MessengerInstance.Send(new NotificationMessage<ChatRoom>(ChatRoomModel, "Add"));

            IsOpen = false;
		}
    }
}
