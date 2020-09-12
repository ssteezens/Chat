namespace Api.Services.Connection.Interfaces
{
    /// <summary>
    ///		Interface for sending messages to exchanges or queues.
    /// </summary>
    public interface IMessageService
	{
        ///  <summary>
        /// 		Send a message to an exchange.
        ///  </summary>
        ///  <param name="exchangeName"> Name of exchange to send to. </param>
        ///  <param name="message"> Message to send. </param>
        ///  <param name="routingKey"> The routing key for the message </param>
        void SendMessageToExchange(string exchangeName, string message, string routingKey = "");

        ///  <summary>
        /// 		Send a message to an exchange.
        ///  </summary>
        ///  <param name="exchangeName"> Name of exchange to send to. </param>
        ///  <param name="message"> Message to send. </param>
        ///  <param name="routingKey"> The routing key for the message. </param>
        void SendMessageToExchange(string exchangeName, object message, string routingKey = "");
	}
}
