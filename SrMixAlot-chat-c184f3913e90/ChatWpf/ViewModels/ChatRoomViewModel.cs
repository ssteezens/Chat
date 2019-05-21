using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ChatWpf.Services.MessageRecieving;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///		View model for a chat room.
    /// </summary>
    public class ChatRoomViewModel : ViewModelBase
    {
        private readonly IChatMessageDataService _chatMessageDataService;

		public ChatRoomViewModel(IChatMessageDataService chatMessageDataService)
		{
			_chatMessageDataService = chatMessageDataService;

			MessageConsumer = new MessageConsumer() { Enabled = true };

			// start receiving messages from the server
			MessageConsumer.Start();

			var backgroundWorker = new BackgroundWorker();

			SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);
            SetUserTextCommand = new RelayCommand<string>(SetUserText, true);
		}

        #region Event Handlers 

		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{

		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{

		}

		private bool CanSendMessage => !string.IsNullOrEmpty(UserText);
		
        public RelayCommand SendMessageCommand { get; }
        public RelayCommand<string> SetUserTextCommand { get; }
		
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
        ///     Sets the user's text.
        /// </summary>
        private void SetUserText(string text)
        {
            UserText = text;
        }

        #endregion

        #region Properties 

        private string _displayName;
		private string _userText;

		public MessageConsumer MessageConsumer { get; set; }

		/// <summary>
        ///		Chat room's display name.
        /// </summary>
		public string DisplayName
		{
			get => _displayName;
			set => Set(ref _displayName, value, nameof(DisplayName));
		}

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
