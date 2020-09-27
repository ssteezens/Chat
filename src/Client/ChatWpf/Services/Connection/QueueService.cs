using ChatWpf.Services.Connection.Interfaces;
using RabbitMQ.Client;

namespace ChatWpf.Services.Connection
{
    /// <summary>
    ///		Service for managing connection queue's.
    /// </summary>
    public class QueueService : IQueueService
	{
		private readonly IConnectionFactory _connectionFactory;
		private readonly IModel _channel;

		public QueueService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
			_channel = _connectionFactory.CreateConnection().CreateModel();
		}

		/// <summary>
		///		Create a connection queue.
		/// </summary>
		/// <param name="queueName"> Name of queue. </param>
		public void CreateQueue(string queueName)
		{
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
