using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ChatEntryController : ControllerBase
    {
        private readonly ChatContext _chatContext;

        public ChatEntryController(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        [HttpPost]
        [Route("/ChatEntry/Add")]
        public void Add([FromBody] ChatEntry entry)
        {
            _chatContext.Add(entry);
        }        

        [HttpGet]
        [Route("/ChatEntry/GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_chatContext.ChatEntrys.ToList());
        }
    }
}