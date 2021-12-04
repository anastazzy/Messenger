using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Messenger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        [HttpGet]
        public string CreateChat()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            return $"Hello, {username}, your id: {userId}";
        }

    }
}
