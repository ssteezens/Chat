using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class TestController : Controller
    {
        private readonly ChatContext _chatContext;

		public TestController(ChatContext chatContext)
		{
			_chatContext = chatContext;
		}

		[HttpGet("/api/test")]
		public IActionResult Test()
		{
			_chatContext.ChatMessages.ToList();

			return Ok();
		}
    }
}
