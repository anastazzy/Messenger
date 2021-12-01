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

            var userInDatabase = await _repository.CreateUserAsync(user);
            var result = DbContext.Users.FirstOrDefaultAsync(x => x.Id == userInDatabase);

            Assert.Equal(userInDatabase, result.Result.Id);
        }

        [Fact]
        public async Task DeleteUser_ShouldDeleteUserAsync()
        {
            var user = new UserDto()
            {
                Username = "waimagi"
            };
            var newUser = new UserDto()
            {
                Username = "lokky"
            };

            await _repository.CreateUserAsync(user);
            var request = await _repository.CreateUserAsync(newUser);
            await _repository.DeleteUserAsync(request);

            Assert.Single(DbContext.Users);
        }

        [Fact]
        public async Task EditUser_ShouldEditUserAsync()
        {
            var user1 = new UserDto()
            {
                Username = "waimagi"
            };
            var user2 = new UserDto()
            {
                Username = "lokky"
            };
            var newUser = new UserDto()
            {
                Username = "lokky"
            };

            await _repository.CreateUserAsync(user1);
            var request = await _repository.CreateUserAsync(user2);
            await _repository.EditUserAsync(request, newUser);
            var result = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == request);

            Assert.Equal(newUser.Username, result.Username);

        }

        [Fact]
        public async Task GetUsers_ShouldGetUserAsync()
        {
            var user = new UserDto()
            {
                Username = "waimagi"
            };

            var userInDatabase = await _repository.CreateUserAsync(user);
            var result = await _repository.GetUsersAsync();


            Assert.Single(result);
        }
    }
}
