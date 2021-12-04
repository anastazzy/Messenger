﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Messenger.Domain;
using Messenger.Domain.Contracts;
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
        
        [HttpGet]
        [Route("{id}")]
        public Task<MessageDto[]> GetListOfChats(Guid id)
        {
            return _repository.GetListOfMessage(id);
        }
        
        [HttpPost]
        public Task<Guid> CreateChat([FromBody]InputChatDto inputChatDto)
        {
            return _repository.CreateChat(inputChatDto, HttpContext.GetUserId());
        }
    }
}
