using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.Controllers
{
    public class ChatRoomController : ControllerBase
    {
        [Route("/ChatRoom/GetAll")]
        public IActionResult GetAll()
        {
            // test user data
            var users = new List<User>()
            {
                new User()
                {
                    NickName = "Test User 1",
                    ImageFilePath = "ms-appx:///Assets/avatar64x64.png"
                },
                new User()
                {
                    NickName = "Test User 2",
                    ImageFilePath = "ms-appx:///Assets/avatar64x64.png"
                },
                new User()
                {
                    NickName = "Test User 3",
                    ImageFilePath = "ms-appx:///Assets/avatar64x64.png"
                }
            };

            // test chat entry data
            var chatEntrys = new List<ChatEntry>()
            {
                new ChatEntry()
                {
                    User = users[1],
                    Message = "Test message 1"
                },
                new ChatEntry()
                {
                    User = users[1],
                    Message = "Test message 2"
                },
                new ChatEntry()
                {
                    User = users[1],
                    Message = "Test message 3"
                },
            };

            // Test available chat rooms
            var chatRooms = new List<ChatRoom>()
            {
                new ChatRoom()
                {
                    DisplayName = "Chat room 1",
                    Users = users,
                    ChatEntrys = chatEntrys,
                    ActiveUser = users[2]
                },
                new ChatRoom()
                {
                    DisplayName = "Chat room 2",
                    Users = users,
                    ChatEntrys = chatEntrys,
                    ActiveUser = users[2]
                },
                new ChatRoom()
                {
                    DisplayName = "Chat room 3",
                    Users = users,
                    ChatEntrys = chatEntrys,
                    ActiveUser = users[2]
                }
            };

            return Ok(chatRooms);
        }
    }
}
