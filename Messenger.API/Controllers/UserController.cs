using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Messenger.Domain;
using Messenger.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public Task RegisterUser(UserDto user)
        {
            return _repository.SaveAsync(user);
        }

        [HttpPost]
        [Route("authenticate")]
        public Task<string> LoginAsync(UserDto user)
        {
            return _repository.LoginAsync(user);
        }
        
    }
}
