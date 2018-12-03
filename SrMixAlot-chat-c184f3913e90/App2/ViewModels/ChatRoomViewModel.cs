using Chat.Models;
using Chat.Services.Data;
using Chat.Services.Data.Interfaces;
using GalaSoft.MvvmLight;
using StructureMap;
using System.Collections.ObjectModel;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using User = Chat.Models.User;

namespace Chat.ViewModels
{
    /// <summary>
    ///     View model for a chat room.
    /// </summary>
    public class ChatRoomViewModel : ViewModelBase
    {
        private string _displayName;
        private string _userText = string.Empty;
		private readonly IChatMessageDataService _chatMessageDataService;

		public ChatRoomViewModel()
		{
			var container = new Container(config =>
			{
				config.For<IChatMessageDataService>().Singleton().Use<ChatMessageDataService>()
					.Named("ChatMessageDataService");
            });
			
			_chatMessageDataService = container.GetInstance<IChatMessageDataService>();
		}

        /// <summary>
        ///     Chat's display name.
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            set => Set(ref _displayName, value, nameof(DisplayName));
        }

        #region Event handlers

        /// <summary>
        ///     Event handler for when the submit button is clicked for the
        ///     ActiveUser's TextBox.
        /// </summary>
        /// <param name="sender"> Submit button. </param>
        /// <param name="e"> Not used. </param>
        public void SubmitClicked(object sender, RoutedEventArgs e)
        {
			SendMessage();
        }

		/// <summary>
        ///		Sends the user's message to the server.
        /// </summary>
		private void SendMessage()
		{
			// create chat entry
			var message = new ChatMessage()
			{
				User = ActiveUser,
				Message = UserText,
				ChatRoomId = Id
			};

			// submit chat to server
			var addedMessage = _chatMessageDataService.AddChatMessage(message);

			// add message to list of messages
			ChatMessages.Add(addedMessage);

			// clear user text
			UserText = string.Empty;
        }
		
		/// <summary>
        ///		Event called when user text changed.
        /// </summary>
        /// <param name="sender"> User text box. </param>
        /// <param name="e"> Not used. </param>
		public void OnUserTextChanged(object sender, TextChangedEventArgs e)
		{
			UserText = ((TextBox) sender).Text;
		}

		/// <summary>
        ///		Event handler for keyboard presses.
        /// </summary>
		public void OnKeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == VirtualKey.Enter)
				SendMessage();
		}

        #endregion

        #region Properties

        /// <summary>
        ///     Text entered into the user's TextBox.
        /// </summary>
        public string UserText
        {
            get => _userText;
            set => Set(ref _userText, value, nameof(UserText));
        }

        /// <summary>
        ///     The active user.
        /// </summary>
        public User ActiveUser { get; set; }

        /// <summary>
        ///     All the chat entry's for this chat pane.
        /// </summary>
        public ObservableCollection<ChatMessage> ChatMessages { get; set; }

        /// <summary>
        ///     All user's in this chat.
        /// </summary>
        public ObservableCollection<User> Users { get; set; }

		/// <summary>
        ///		Id of the chat room.
        /// </summary>
		public int Id { get; set; }

        #endregion
    }
}
