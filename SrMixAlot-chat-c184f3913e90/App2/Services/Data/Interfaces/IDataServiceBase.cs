using ServiceStack;

namespace Chat.Services.Data.Interfaces
{
    /// <summary>
    ///     Data service used for making api requests.
    /// </summary>
    public interface IDataServiceBase
	{	
        /// <summary>
        ///     The client used for api requests.
        /// </summary>
        JsonServiceClient Client { get; set; }
    }
}
