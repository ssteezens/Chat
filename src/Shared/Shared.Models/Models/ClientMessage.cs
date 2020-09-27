namespace Shared.Models.Models
{
    /// <summary>
    ///     Defines a client message.
    /// </summary>
    /// <typeparam name="T"> The type of model to send to a client. </typeparam>
    public class ClientMessage<T>
    {
        public ClientMessage(T model)
        {
            Model = model;
        }

        /// <summary>
        ///     The client message model.
        /// </summary>
        public T Model { get; set; }

        /// <summary>
        ///     The type of operation of the message.
        /// </summary>
        public MessageOperationTypes OperationType { get; set; }
    }
}
