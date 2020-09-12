using System;
using Api.Services.Connection.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Api.Services.Connection
{
	/// <summary>
    ///		Service for sending messages to exchanges.
    /// </summary>
    public class MessageService : IMessageService
	{
		private readonly IConnectionFactory _connectionFactory;
        private readonly IModel _channel;

        public MessageService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
			_channel = _connectionFactory.CreateConnection().CreateModel();
        }

        ///  <summary>
        /// 		Send a message to an exchange.
        ///  </summary>
        ///  <param name="exchangeName"> Name of exchange to send to. </param>
        ///  <param name="message"> Message to send. </param>
        ///  <param name="routingKey"> The routing key for the message. </param>
        public void SendMessageToExchange(string exchangeName, string message, string routingKey = "")
		{
			var properties = _channel.CreateBasicProperties();
			var messageBuffer = Encoding.Default.GetBytes(message);

            _channel.BasicPublish(exchangeName, string.Empty, properties, messageBuffer);
		}

		///  <summary>
		/// 		Send a message to an exchange.
		///  </summary>
		///  <param name="exchangeName"> Name of exchange to send to. </param>
		///  <param name="message"> Message to send. </param>
		///  <param name="routingKey"> The routing key for the message. </param>
		public void SendMessageToExchange(string exchangeName, object message, string routingKey = "")
		{
			var properties = _channel.CreateBasicProperties();
			var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            var messageJson = JsonConvert.SerializeObject(message, typeof(object), settings);
			var messageBuffer = Encoding.Default.GetBytes(messageJson);

			_channel.BasicPublish(exchangeName, string.Empty, properties, messageBuffer);
        }
    }
}
