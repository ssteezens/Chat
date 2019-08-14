using Api.Models.Entities;
using Api.Services.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Shared.Models.Dto;

namespace Api.Services.Data
{
    /// <summary>
    ///     Service chat message related data.
    /// </summary>
    public class ChatMessageDataService : IChatMessageDataService
	{
		private readonly ChatContext _chatContext;
        private readonly IMapper _mapper;

		public ChatMessageDataService(ChatContext chatContext, IMapper mapper)
		{
			_chatContext = chatContext;
			_mapper = mapper;
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
        ///     Deletes a chat message.
        /// </summary>
        /// <param name="id"> Id of the chat message to delete. </param>
        public void Delete(int id)
        {
            _chatContext.ChatMessages.Remove(_chatContext.ChatMessages.Single(i => i.Id == id));

            _chatContext.SaveChanges();
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
