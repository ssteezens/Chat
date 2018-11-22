using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ChatMessageController : ControllerBase
    {
        private readonly ChatContext _chatContext;

        public ChatMessageController(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        [HttpPost]
        [Route("/ChatMessage/Add")]
        public ChatMessage Add([FromBody] ChatMessage message)
		{
			var addedMessage = _chatContext.Add(message).Entity;

			_chatContext.SaveChanges();

			return addedMessage;
		}        

        [HttpGet]
        [Route("/ChatMessage/GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_chatContext.ChatMessages.ToList());
        }
    }
}