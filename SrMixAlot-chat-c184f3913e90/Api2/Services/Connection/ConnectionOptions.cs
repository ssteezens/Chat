namespace Api.Services.Connection
{
    /// <summary>
    ///     Options for connecting to rabbit mq.
    /// </summary>
    public class ConnectionOptions
    {
        public const string Connection = "RabbitMq";

        /// <summary>
        ///     The host name of the connection.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        ///     The username of the connection.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     The password of the connection.
        /// </summary>
        public string Password { get; set; }
    }
}
