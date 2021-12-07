using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Messenger.Domain;
using Messenger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Messenger.Infrastructure.Repository
{
    public class UserRepository
    {
        private readonly MessengerDbContext _dbContext;

        public UserRepository(MessengerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> SaveAsync(UserDto userDto)
        {
            var user = new User
            {
                PasswordHash = GetHash(userDto.Password),
                Username = userDto.Username
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }

        public static string GetHash(string password)
        {
            var hash = new StringBuilder();
            using var hashAlgorithm = new SHA256Managed();

            byte[] crypto = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (byte theByte in crypto)
                hash.Append(theByte.ToString("x2"));

            return hash.ToString();
        }

        public async Task<string> LoginAsync(UserDto user)
        {
            var result = await _dbContext.Users.FirstAsync(x => x.Username == user.Username && x.PasswordHash == GetHash(user.Password));
            return GenerateAccessJwtToken(result.Id.ToString(), result.Username);
        }

        private static string GenerateAccessJwtToken(string userId, string username)
        {
            const string signingAlgorithm = SecurityAlgorithms.HmacSha256;
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("qwerq4r45y7646c4HBREUy"));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Sid, userId),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.MaxValue,
                signingCredentials: new SigningCredentials(securityKey, signingAlgorithm)
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        
    }
}
