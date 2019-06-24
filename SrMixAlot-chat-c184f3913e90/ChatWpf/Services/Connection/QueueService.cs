using ChatWpf.Services.Connection.Interfaces;
using RabbitMQ.Client;

namespace ChatWpf.Services.Connection
{
    /// <summary>
    ///		Service for managing connection queue's.
    /// </summary>
    public class QueueService : IQueueService
	{
		private const string HostName = "localhost";
		private const string UserName = "guest";
		private const string Password = "guest";

		private readonly ConnectionFactory _connectionFactory;
		private readonly IModel _channel;

		public QueueService()
		{
			_connectionFactory = new ConnectionFactory
			{
				HostName = HostName,
				UserName = UserName,
				Password = Password
			};

			_channel = _connectionFactory.CreateConnection().CreateModel();
		}

		/// <summary>
		///		Create a connection queue.
		/// </summary>
		/// <param name="queueName"> Name of queue. </param>
		public void CreateQueue(string queueName)
		{
			// todo: error handling
			_channel.QueueDeclare(queueName, true, false, true, null);
		}

		/// <summary>
		///		Binds a queue to an exchange.
		/// </summary>
		/// <param name="queueName"> The queue to bind. </param>
		/// <param name="exchangeName"> The exchange to bind to. </param>
		/// <param name="routingKey"> The routing key. </param>
		public void BindToExchange(string queueName, string exchangeName, string routingKey)
		{
			_channel.QueueBind(queueName, exchangeName, routingKey);
		}
	}
}
