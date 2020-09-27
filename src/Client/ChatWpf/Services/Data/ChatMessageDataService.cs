using System.Collections.Generic;
using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using ServiceStack;
using Shared.Models.Models;

namespace ChatWpf.Services.Data
{
    /// <summary>
    ///		Service for chat message related data.
    /// </summary>
    public class ChatMessageDataService : IChatMessageDataService
    {
        private readonly IJsonServiceClient _jsonServiceClient;

		public ChatMessageDataService(IJsonServiceClient jsonServiceClient)
		{
			_jsonServiceClient = jsonServiceClient;
		}

		/// <summary>
		///		Add a chat entry to a chat room.
		/// </summary>
		/// <returns> True if successful. </returns>
		public ChatMessage Add(ChatMessage message)
		{
			return _jsonServiceClient.Post<ChatMessage>("/ChatMessage/Add", message);
		}

        /// <summary>
        ///     Edit a chat message.
        /// </summary>
        /// <param name="message"> The <see cref="ChatMessage"/> to edit. </param>
        /// <returns> The edited chat message. </returns>
        public ChatMessageModel Edit(ChatMessage message)
        {
            return _jsonServiceClient.Post<ChatMessageModel>("/ChatMessage/Update", message);
        }

        /// <summary>
        ///     Deletes a chat message.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _jsonServiceClient.Get<IReturnVoid>($"/ChatMessage/Delete/{id}");
        }

		/// <summary>
		///     Get an enumerable of chat entries.
		/// </summary>
		/// <returns> List of chat entries. </returns>
		public IEnumerable<ChatMessage> GetChatEntries()
		{
			return _jsonServiceClient.Get<IEnumerable<ChatMessage>>("/ChatMessage/GetAll");
		}
    }
}
