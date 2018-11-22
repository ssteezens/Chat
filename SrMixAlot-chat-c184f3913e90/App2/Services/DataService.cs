using Chat.Models;
using ServiceStack;
using System.Collections.Generic;
using System.Net;
using Chat.Services.Interfaces;

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
			// TODO: wire this up using middleware
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

		/// <summary>
        ///		Add a chat entry to a chat room.
        /// </summary>
        /// <returns> True if successful. </returns>
		public bool AddChatMessage(ChatMessage message)
		{
			return Client.Post<bool>("/ChatMessage/Add", message);
		}

        /// <summary>
        ///     Get an enumerable of chat entries.
        /// </summary>
        /// <returns> List of chat entries. </returns>
        public IEnumerable<ChatMessage> GetChatEntries()
        {
            return Client.Get<IEnumerable<ChatMessage>>("/ChatMessage/GetAll");
        }

        /// <summary>
        ///     Get an enumerable of chat rooms.
        /// </summary>
        /// <returns> Enumerable of chat rooms. </returns>
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
