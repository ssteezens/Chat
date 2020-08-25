using Api.Models.Entities;
using Api.Services.Data.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Data
{
    /// <summary>
    ///		Service for chat room related data.
    /// </summary>
    public class ChatRoomDataService : IChatRoomDataService
	{
		private readonly ChatContext _chatContext;
		private readonly IMapper _mapper;

		///  <summary>
		/// 		Chat room constructor.
		///  </summary>
		///  <param name="chatContext"> Ef core chat context. </param>
		///  <param name="mapper"> Injected AutoMapper. </param>
		public ChatRoomDataService(ChatContext chatContext, IMapper mapper)
		{
			_chatContext = chatContext;
			_mapper = mapper;
		}

		/// <summary>
		///		Gets all chat rooms from the database.
		/// </summary>
		/// <returns> All chat rooms from the database.</returns>
		public IEnumerable<ChatRoom> GetAll()
		{
            var chatRooms = _chatContext.ChatRooms
                .Include(room => room.ChatMessages)
                .ThenInclude(message => message.User);

            return chatRooms;
		}

		/// <summary>
		///		Gets all chat rooms from the database.
		/// </summary>
		/// <returns> All chat rooms from the database.</returns>
		public IEnumerable<ChatRoom> GetAll(string username)
		{
			var chatRooms = _chatContext.ChatRooms
				.Include(room => room.ChatMessages)
                .ThenInclude(message => message.User)
				.Where(room => room.Users.Any(user => user.UserName == username));

            return chatRooms;
		}

		/// <summary>
		///		Add a chat room to the database.
		/// </summary>
		/// <param name="chatRoom"> Chat room to add. </param>
		public ChatRoom Add(ChatRoom chatRoom)
		{
			_chatContext.ChatRooms.Add(chatRoom);
			_chatContext.SaveChanges();

			return chatRoom;
		}

		/// <summary>
		///		Add a user to the chat room.
		/// </summary>
		/// <param name="username"> The username to add. </param>
		/// <param name="chatRoomId"> The id of the chat room. </param>
        public void AddUser(string username, int chatRoomId)
        {
            var user = _chatContext.Users.SingleOrDefault(i => i.UserName == username);
            var room = _chatContext.ChatRooms.Find(chatRoomId);

			_chatContext.Entry(room).Collection(i => i.Users).Load();

            room.Users.Add(user);

            _chatContext.SaveChanges();
        }
    }
}
