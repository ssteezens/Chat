using ChatWpf.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///     View model for adding a chat room.
    /// </summary>
    public class AddChatRoomViewModel : ViewModelBase
    {
        public AddChatRoomViewModel()
        {
            // initialize chat room
            ChatRoomModel = new ChatRoom()
            {
                Users = new List<User>() { UserInstance.Current },
                ChatMessages = new List<ChatMessage>()
            };

            // initialize commands
            SubmitCommand = new RelayCommand(Submit, true);
        }

        #region Properties

        private bool _isOpen;

        /// <summary>
        ///		Opens or closes the view.
        /// </summary>
        public bool IsOpen
        {
            get => _isOpen;
            set => Set(ref _isOpen, value, nameof(IsOpen));
        }

		/// <summary>
        ///		The chat room model.
        /// </summary>
        public ChatRoom ChatRoomModel { get; set; }

        #endregion

        #region Commands

		/// <summary>
        ///		Command for when the submit button is pressed.
        /// </summary>
        public RelayCommand SubmitCommand { get; }

		/// <summary>
        ///		Send chat room to main view model.
        /// </summary>
        private void Submit()
        {
            // todo: validation

            ChatRoomModel.Users = new List<User> { UserInstance.Current };

            // send event to be handled on main view model
            MessengerInstance.Send(new NotificationMessage<ChatRoom>(ChatRoomModel, "Add"));

            IsOpen = false;
        }

        #endregion
    }
}
