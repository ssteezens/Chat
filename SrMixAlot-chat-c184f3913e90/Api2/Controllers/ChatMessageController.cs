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
        public void Add([FromBody] ChatMessage message)
		{
            _chatContext.Add(message);
        }        

        [HttpGet]
        [Route("/ChatMessage/GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_chatContext.ChatMessages.ToList());
        }
    }
}