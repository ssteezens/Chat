using Api.Services.Connection.Interfaces;
using RabbitMQ.Client;

namespace Api.Services.Connection
{
	/// <summary>
    ///		Service for managing exchanges.
    /// </summary>
    public class ExchangeService : IExchangeService
    {
		private const string HostName = "localhost";
		private const string UserName = "guest";
		private const string Password = "guest";

		private readonly ConnectionFactory _connectionFactory;
		private readonly IModel _channel;

		public ExchangeService()
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
        ///		Create an exchange.
        /// </summary>
        /// <param name="exchangeName"> Name of exchange to create. </param>
        public void CreateExchange(string exchangeName)
		{
			_channel.ExchangeDeclare(exchangeName, "fanout", true, false, null);
		}
	}
}
