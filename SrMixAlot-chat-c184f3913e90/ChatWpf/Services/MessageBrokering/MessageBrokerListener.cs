using System;
using System.Text;
using AutoMapper;
using ChatWpf.Models;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using Shared.Models.Dto;

namespace ChatWpf.Services.MessageBrokering
{
	/// <summary>
    ///		Listener for messages sent by message broker.
    /// </summary>
    public class MessageBrokerListener : IDisposable
    {
		private const string HostName = "localhost";
		private const string UserName = "guest";
		private const string Password = "guest";

		private ConnectionFactory _connectionFactory;
		private readonly IConnection _connection;
		private readonly IModel _model;
		private Subscription _subscription;
		
        public MessageBrokerListener()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password
            };

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
                var messageString = Encoding.Default.GetString(deliveryArgs.Body);
				var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
				var receivedMessage = JsonConvert.DeserializeObject(messageString, settings);

				switch (receivedMessage)
				{
                    case ChatMessageDto chatMessageDto:
					{
						var chatMessage = Mapper.Map<ChatMessage>(chatMessageDto);
						// send chat message to view model
						Messenger.Default.Send(new NotificationMessage<ChatMessage>(chatMessage, chatMessageDto.OperationType.ToString()));
						break;
					}
                    case ChatRoomDto chatRoomDto:
					{
						var chatRoom = Mapper.Map<ChatRoom>(chatRoomDto);
						Messenger.Default.Send(new NotificationMessage<ChatRoom>(chatRoom, chatRoomDto.OperationType.ToString()));
						break;
					}
                    default:
						throw new NotSupportedException("Unsupported message type sent to handler.");
				}
				
                // acknowledge message is processed
                _subscription.Ack(deliveryArgs);
            }
        }

        #region Properties
		
		/// <summary>
        ///		Gets or sets whether the message consumer is enabled.
        /// </summary>
		public bool Enabled { get; set; }

		/// <summary>
        ///		Name of queue.
        /// </summary>
		public string QueueName { get; set; } = "Module2.Sample4.Queue1";
		
        #endregion

        public void Dispose()
		{
			_model?.Dispose();
			_connection?.Dispose();

			_connectionFactory = null;

			GC.SuppressFinalize(this);
        }
	}
}
