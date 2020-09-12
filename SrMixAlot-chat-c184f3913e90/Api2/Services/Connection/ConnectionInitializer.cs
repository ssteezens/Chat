using Data.Services.Interfaces;
using RabbitMQ.Client;

namespace Api.Services.Connection
{
    /// <summary>
    ///		Initialize message broker connections.  Ensure an exchange is created for each chat room.
    /// </summary>
    public class ConnectionInitializer
    {
		private readonly IChatRoomDataService _chatRoomDataService;
        private readonly IConnectionFactory _connectionFactory;

        public ConnectionInitializer(IChatRoomDataService chatRoomDataService, IConnectionFactory connectionFactory)
		{
			_chatRoomDataService = chatRoomDataService;
            _connectionFactory = connectionFactory;
        }

		/// <summary>
        ///		Initialize rabbitmq.  Ensure main exchange is created, queue's
        ///		are created for each room, and bound to main exchange.
        /// </summary>
		public void Initialize()
		{
			var rooms = _chatRoomDataService.GetAll();

			using (var channel = _connectionFactory.CreateConnection().CreateModel())
			{
				foreach (var room in rooms)
				{
					// ensure an exchange is created for each room
					channel.ExchangeDeclare($"Chat.Room.{room.Id}", "fanout", true,  false, null);
				}
			}
        }
    }
}
