using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Text;

namespace ChatWpf.Services.MessageRecieving
{
    public class MessageConsumer : IDisposable
    {
		private const string HostName = "localhost";
		private const string UserName = "guest";
		private const string Password = "guest";
		private const string QueueName = "Module2.Sample4.Queue1";
		private const string VirtualHost = "";
		private int Port = 0;

		private ConnectionFactory _connectionFactory;
		private IConnection _connection;
		private IModel _model;
		private Subscription _subscription;
		
        /// <summary>
        ///		Ctor with a key to lookup the configuration
        /// </summary>
        public MessageConsumer()
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
            _model.BasicQos(0, 1, false);
        }
		
        /// <summary>
        ///		Starts receiving a message from a queue
        /// </summary>
        public void Start()
        {
            _subscription = new Subscription(_model, QueueName, false);

            var consumer = new ConsumeDelegate(Poll);
            consumer.Invoke();
        }
        private delegate void ConsumeDelegate();

		/// <summary>
        ///		Poll for messages sent to queue.
        /// </summary>
        private void Poll()
        {
            while (Enabled)
            {
                //Get next message
                var deliveryArgs = _subscription.Next();
                //Deserialize message
                var message = Encoding.Default.GetString(deliveryArgs.Body);

                //Handle Message
                Console.WriteLine("Message Recieved - {0}", message);

                //Acknowledge message is processed
                _subscription.Ack(deliveryArgs);
            }
        }

        #region Properties

		public delegate void OnReceiveMessage(string message);

		/// <summary>
        ///		Gets or sets whether the message consumer is enabled.
        /// </summary>
		public bool Enabled { get; set; }

        #endregion

        public void Dispose()
		{
			if (_model != null)
				_model.Dispose();
			if (_connection != null)
				_connection.Dispose();

			_connectionFactory = null;

			GC.SuppressFinalize(this);
        }
	}
}
