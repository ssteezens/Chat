using Api.Models.Entities;
using Api.Services.Data.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

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
            // todo: change the model on the frontend to send a model like this
            message.UserId = message.User.Id;
            message.User = null;
			var addedMessage = _chatContext.ChatMessages.Add(message).Entity;

			_chatContext.SaveChanges();
			
			return addedMessage; 
        }
        
        /// <summary>
        ///     Deletes a chat message.
        /// </summary>
        /// <param name="id"> Id of the chat message to delete. </param>
        public ChatMessage Delete(int id)
        {
            var deletedMessage = _chatContext.ChatMessages.Remove(_chatContext.ChatMessages.Single(i => i.Id == id)).Entity;

            _chatContext.SaveChanges();

            return deletedMessage;
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
