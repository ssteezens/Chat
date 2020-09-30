using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Data.Services.Interfaces;
using Shared.Models.Models;

namespace Data.Services
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
                .Include(room => room.UserRooms)
                .ThenInclude(userRoom => userRoom.User)
				.Include(room => room.ChatMessages)
                .ThenInclude(message => message.User)
				.Where(room => room.UserRooms.Select(userRoom => userRoom.User).Any(user => user.UserName == username))
                .ToList();

            return chatRooms;
		}

		/// <summary>
		///		Add a chat room to the database.
		/// </summary>
		/// <param name="chatRoomModel"> Chat room to add. </param>
		public ChatRoomModel Add(ChatRoomModel chatRoomModel)
        {
            var chatRoom = new ChatRoom() { DisplayName = chatRoomModel.DisplayName };

            _chatContext.ChatRooms.Add(chatRoom);
			_chatContext.SaveChanges();
            _chatContext.Entry(chatRoom).Collection(i => i.UserRooms).Load();

			// set dto model's id
            chatRoomModel.Id = chatRoom.Id;

            foreach (var user in chatRoomModel.Users)
            {
                var userRoom = new UserRoom()
                {
                    UserId = user.Id,
                    ChatRoomId = chatRoom.Id
                };

				chatRoom.UserRooms.Add(userRoom);
            }

            _chatContext.SaveChanges();

            return chatRoomModel;
		}

        /// <summary>
        ///     Deletes a chat room in the database.
        /// </summary>
        /// <param name="id"> Id of the chat room to delete. </param>
        /// <returns> The deleted chat room. </returns>
        public ChatRoom Delete(int id)
        {
            var deletedRoom = _chatContext.ChatRooms.Remove(_chatContext.ChatRooms.SingleOrDefault(i => i.Id == id))
                .Entity;

            _chatContext.SaveChanges();

            return deletedRoom;
        }

        /// <summary>
		///		Add a user to the chat room.
		/// </summary>
		/// <param name="username"> The username to add. </param>
		/// <param name="chatRoomId"> The id of the chat room. </param>
        public UserRoom AddUser(string username, int chatRoomId)
        {
            var user = _chatContext.Users.SingleOrDefault(i => i.UserName == username);
            var room = _chatContext.ChatRooms.Find(chatRoomId);

            if (user == null)
                throw new InvalidOperationException("User could not be found.");

			_chatContext.Entry(room).Collection(i => i.UserRooms).Load();

			var userRoom = new UserRoom()
            {
                ChatRoomId = room.Id,
				UserId = user.Id
            };

			if(room.UserRooms.All(i => i.UserId != userRoom.UserId))
                room.UserRooms.Add(userRoom);

            _chatContext.SaveChanges();

            return userRoom;
        }

        /// <summary>
        ///     Removes a user to chat room association in the database.
        /// </summary>
        /// <param name="username"> Username of the user to chat room relation. </param>
        /// <param name="chatRoomId"> The id of the chat room. </param>
        /// <returns> The removed user to chat room relation. </returns>
        public UserRoom RemoveUser(string username, int chatRoomId)
        {
            var user = _chatContext.Users.SingleOrDefault(i => i.UserName == username);
            
            if (user == null)
                throw new InvalidOperationException("User does not exist");

            var entity = _chatContext.ChatUserRooms.FirstOrDefault(i => i.ChatRoomId == chatRoomId && i.UserId == user.Id);

            if(entity == null)
                throw new InvalidOperationException("User does not exist in room");
                
            var removedUserRoom = _chatContext.ChatUserRooms.Remove(entity);

            _chatContext.SaveChanges();

            var room = _chatContext.ChatRooms.SingleOrDefault(i => i.Id == chatRoomId);
            var deletedEntity = removedUserRoom.Entity;
            deletedEntity.ChatRoom = room;

            return deletedEntity;
        }
    }
}
