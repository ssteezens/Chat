using System.Collections.Generic;
using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using ServiceStack;

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
