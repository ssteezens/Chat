namespace Api.Services.Connection.Interfaces
{
    /// <summary>
    ///		Service for managing exchanges.
    /// </summary>
    public interface IExchangeService
	{
		/// <summary>
        ///		Create an exchange.
        /// </summary>
        /// <param name="exchangeName"> Name of exchange to create. </param>
		void CreateExchange(string exchangeName);
	}
}
