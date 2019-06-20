namespace ChatWpf.Services.Connection.Interfaces
{
    /// <summary>
    ///		Service for managing connection queue's.
    /// </summary>
    public interface IQueueService
	{
		/// <summary>
		///		Create a connection queue.
		/// </summary>
		/// <param name="queueName"> Name of queue. </param>
		void CreateQueue(string queueName);

		/// <summary>
		///		Binds a queue to an exchange.
		/// </summary>
		/// <param name="queueName"> The queue to bind. </param>
		/// <param name="exchangeName"> The exchange to bind to. </param>
		/// <param name="routingKey"> The routing key. </param>
		void BindToExchange(string queueName, string exchangeName, string routingKey);
	}
}
