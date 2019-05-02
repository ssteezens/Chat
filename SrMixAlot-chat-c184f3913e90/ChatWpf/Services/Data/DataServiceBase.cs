using System.Net;
using ChatWpf.Services.Data.Interfaces;
using ServiceStack;

namespace ChatWpf.Services.Data
{
    /// <summary>
    ///     Data service used for making api requests.
    /// </summary>
    public class DataServiceBase : IDataServiceBase
    {
        public DataServiceBase()
        {
            Client = new JsonServiceClient("https://localhost:5001");

            // TODO: refine this, maybe filter on sender
			// TODO: wire this up using middleware
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        ///     The client used for api requests.
        /// </summary>
        public JsonServiceClient Client { get; set; }
    }
}
