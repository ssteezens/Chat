using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Services.Data.Interfaces;

namespace Api.Services.Data
{
    /// <summary>
    ///     Service chat message related data.
    /// </summary>
    public class ChatMessageDataService : IChatMessageDataService
	{
		private readonly ChatContext _chatContext;

		public ChatMessageDataService(ChatContext chatContext)
		{
			_chatContext = chatContext;
		}

		/// <summary>
		///		Adds a chat message to the database.
		/// </summary>
		/// <param name="message"> Message to add to the database. </param>
		/// <returns></returns>
		public ChatMessage Add(ChatMessage message)
		{
			var addedMessage = _chatContext.Add(message).Entity;

			_chatContext.SaveChanges();

			return addedMessage;
        }

		/// <summary>
		///		Gets all chat messages from the database.
		/// </summary>
		/// <returns> All chat messages from the database. </returns>
		public IEnumerable<ChatMessage> GetAll()
		{
			return _chatContext.ChatMessages.ToList();

		}
	}
}
