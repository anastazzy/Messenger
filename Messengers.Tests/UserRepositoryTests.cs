using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.Domain.Contracts;
using Messenger.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Messengers.Tests
{
    [Trait("Tests", "UserRepository")]
    public class UserRepositoryTests: WithInMemoryDataBase
    {
        private readonly UserRepository _repository;

        public UserRepositoryTests()
        {
            _repository =new UserRepository(DbContext);
        }

        [Fact]
        public async Task CreateUser_ShouldCreateUserAsync()
        {
            var user = new UserDto()
            {
                Username = "waimagi"
            };

            var userInDatabase = await _repository.SaveAsync(user);
            var result = DbContext.Users.FirstOrDefaultAsync(x => x.Id == userInDatabase);

            Assert.Equal(userInDatabase, result.Result.Id);
        }

    }
}
