using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace ChatWpf.Models
{
    /// <summary>
    ///     Wrapper class to provide common actions to list items.
    /// </summary>
    /// <typeparam name="T"> The model type of the list item. </typeparam>
    public class ActionListItem<T> : ObservableObject
    {

        public ActionListItem()
        {
            MessengerInstance = Messenger.Default;
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
        }

        public ActionListItem(T model, IMessenger messengerInstance, object listenerToken = null)
        {
            Model = model;
            MessengerInstance = messengerInstance;
            ListenerToken = listenerToken;

            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
        }

        /// <summary>
        ///     The list item model.
        /// </summary>
        public T Model { get; set; }

        /// <summary>
        ///     The <see cref="IMessenger"/> instance to send view model messages on.
        /// </summary>
        public IMessenger MessengerInstance { get; set; }

        /// <summary>
        ///     The listener token to send with view model messages.
        /// </summary>
        public object ListenerToken { get; set; }

        /// <summary>
        ///     Generic edit command.
        /// </summary>
        public RelayCommand EditCommand { get; }

        /// <summary>
        ///     Generic delete command. 
        /// </summary>
        public RelayCommand DeleteCommand { get; }

        /// <summary>
        ///     Sends an "Edit" notification message.
        /// </summary>
        public void Edit()
        {
            SendMessage(new NotificationMessage<T>(Model, "Edit"));
        }

        /// <summary>
        ///     Sends a "Delete" notification message.
        /// </summary>
        public void Delete()
        {
            SendMessage(new NotificationMessage<T>(Model, "Delete"));
        }

        /// <summary>
        ///     Send the specified notification message with the ListenerToken if specified.
        /// </summary>
        /// <param name="message"> The notification message to send. </param>
        private void SendMessage(NotificationMessage<T> message)
        {
            if(ListenerToken != null)
                MessengerInstance.Send(message, ListenerToken);
            else 
                MessengerInstance.Send(message);
        }
    }
}
