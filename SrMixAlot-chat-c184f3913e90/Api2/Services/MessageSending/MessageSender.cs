using RabbitMQ.Client;
using System;
using System.Text;

namespace Api.Services.MessageSending
{
    public class MessageSender : IDisposable
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string ExchangeName = "Module2.Sample4.Exchange";
        private const bool IsDurable = true;
        private const string VirtualHost = "";
        private int Port = 0;

        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;

        /// <summary>
        /// Ctor
        /// </summary>
        public MessageSender()
        {
            SetupRabbitMq();
        }

        /// <summary>
        /// Sets up the connections for rabbitMQ
        /// </summary>
        private void SetupRabbitMq()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password
            };

            if (string.IsNullOrEmpty(VirtualHost) == false)
                _connectionFactory.VirtualHost = VirtualHost;
            if (Port > 0)
                _connectionFactory.Port = Port;

            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
        }

        public void Send(string message, string routingKey)
        {
            //Setup properties
            var properties = _model.CreateBasicProperties();
            properties.SetPersistent(true);

            //Serialize
            byte[] messageBuffer = Encoding.Default.GetBytes(message);

            //Send message
            _model.BasicPublish(ExchangeName, routingKey, properties, messageBuffer);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_connection != null)
                _connection.Close();

            if (_model != null && _model.IsOpen)
                _model.Abort();

            _connectionFactory = null;

            GC.SuppressFinalize(this);
        }
	}
}
