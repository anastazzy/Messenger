using System;
using System.Threading.Tasks;
using Messenger.Domain.Contracts;
using Messenger.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Messenger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController: ControllerBase
    {
        private readonly MessageRepository _repository;

        public MessageController(MessageRepository repository)
        {
            _repository = repository;
        }

       
    }
}