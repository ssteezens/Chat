using RabbitMQ.Client;

namespace Api.Services.Connection
{
    /// <summary>
    ///		Initialize message broker connections.  Ensure an exchange is created for each chat room.
    /// </summary>
    public class ConnectionInitializer
    {
        private readonly IConnectionFactory _connectionFactory;

        public ConnectionInitializer(IConnectionFactory connectionFactory)
		{
            _connectionFactory = connectionFactory;
        }

		/// <summary>
        ///		Initialize rabbitmq.  Ensure main exchange is created, queue's
        ///		are created for each room, and bound to main exchange.
        /// </summary>
		public void Initialize()
		{
			using (var channel = _connectionFactory.CreateConnection().CreateModel())
			{
                // ensure RabbitMq exchange is created
                channel.ExchangeDeclare("Chat.Room.RoomId", "fanout", true, false, null);
			}
        }
    }
}
