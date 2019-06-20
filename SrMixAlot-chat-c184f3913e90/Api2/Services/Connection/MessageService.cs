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
		private const string HostName = "localhost";
		private const string UserName = "guest";
		private const string Password = "guest";
		
		private readonly ConnectionFactory _connectionFactory;
        private readonly IModel _channel;

        public MessageService()
		{
			// todo: inherit connection from shared object
			// todo: get connection settings from configuration
			_connectionFactory = new ConnectionFactory
			{
				HostName = HostName,
				UserName = UserName,
				Password = Password
			};

			_channel = _connectionFactory.CreateConnection().CreateModel();
        }

		/// <summary>
		///		Send a message to an exchange.
		/// </summary>
		/// <param name="exchangeName"> Name of exchange to send to. </param>
		/// <param name="message"> Message to send. </param>
        public void SendMessageToExchange(string exchangeName, string message)
		{
			var properties = _channel.CreateBasicProperties();
			var messageBuffer = Encoding.Default.GetBytes(message);

            _channel.BasicPublish(exchangeName, string.Empty, properties, messageBuffer);
		}

		/// <summary>
		///		Send a message to an exchange.
		/// </summary>
		/// <param name="exchangeName"> Name of exchange to send to. </param>
		/// <param name="message"> Message to send. </param>
        public void SendMessageToExchange(string exchangeName, object message)
		{
			var properties = _channel.CreateBasicProperties();
			var messageJson = JsonConvert.SerializeObject(message);
			var messageBuffer = Encoding.Default.GetBytes(messageJson);

			_channel.BasicPublish(exchangeName, string.Empty, properties, messageBuffer);
        }
    }
}
