using System.Collections.Generic;
using Chat.Models;
using ServiceStack;

namespace Chat.Services.Interfaces
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
