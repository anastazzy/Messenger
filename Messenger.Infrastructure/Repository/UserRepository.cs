using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Messenger.Domain;
using Messenger.Domain.Contracts;
using Messenger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastructure.Repository
{
    public class UserRepository
    {
        private readonly MessengerDbContext _dbContext;

        public UserRepository(MessengerDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Guid> CreateUserAsync(UserDto userDto)
        {
            var user = new User()
            {
                Username = userDto.Username
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return false;
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditUserAsync(Guid id, UserDto userDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return false;
            user.Username = userDto.Username;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto[]> GetUsersAsync()
        {
            return await _dbContext.Users.Select(x => new UserDto()
            {
                Username = x.Username
            }).ToArrayAsync();
        }

    }
}
