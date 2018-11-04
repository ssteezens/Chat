using Chat.Models;
using ServiceStack;
using System.Collections.Generic;
using System.Net;

namespace Chat.Services
{
    /// <summary>
    ///     Data service used for making api requests.
    /// </summary>
    public class DataServiceBase : IDataService
    {
        public DataServiceBase()
        {
            Client = new JsonServiceClient("https://localhost:44382");

            // TODO: refine this, maybe filter on sender
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        ///     Get an enumerable of chat entries.
        /// </summary>
        /// <returns> List of chat entries. </returns>
        public IEnumerable<ChatEntry> GetChatEntries()
        {
            return Client.Get<IEnumerable<ChatEntry>>("/ChatEntry/GetAll");
        }

        /// <summary>
        ///     Get an enumerable of chat rooms.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ChatRoom> GetChatRooms()
        {
            return Client.Get<IEnumerable<ChatRoom>>("/ChatRoom/GetAll");
        }
        
        /// <summary>
        ///     The client used for api requests.
        /// </summary>
        public JsonServiceClient Client { get; set; }
    }
}
