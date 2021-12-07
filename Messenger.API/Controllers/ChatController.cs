using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Messenger.Domain;
using Messenger.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Messenger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly ChatRepository _repository;
        public ChatController(ChatRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost]
        public Task<Guid> CreateChat([FromBody]InputChatDto inputChatDto)
        {
            return _repository.CreateChat(inputChatDto, HttpContext.GetUserId());
        }
        
        [HttpGet]
        public Task<ListOfChatDto[]> GetListOfChats()
        {
            return _repository.GetListOfChats(HttpContext.GetUserId());
        }
        
        
        [HttpGet]
        [Route("{id}")]
        public Task<OutputMessageDto[]> GetChatMessages(Guid id)
        {
            return _repository.GetChatMessages(id);
        }

        [HttpPost]
        [Route("{id}")]
        public Task<Guid> CreateMessageAsync([FromBody]InputMessageDto messageDto, Guid id)
        {
            return _repository.CreateMessageAsync(messageDto, HttpContext.GetUserId(), id);
        }
    }
}
