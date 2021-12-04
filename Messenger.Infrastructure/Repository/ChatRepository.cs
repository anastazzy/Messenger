using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.Domain;
using Messenger.Infrastructure.Data;
using Messenger.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Messenger.Infrastructure.Repository
{
    public class ChatRepository
    {
        private readonly MessengerDbContext _dbContext;

        public ChatRepository(MessengerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<MessageDto[]> GetListOfMessage(Guid id)
        {
            var messages =  _dbContext.Messages
                .Where(x => x.ChatId == id)
                .OrderBy(x => x.CreationTime)
                .Select(x => new MessageDto()
                {
                    CreationTime = x.CreationTime,
                    Status = x.Status,
                    Text = x.Text,
                    Username = x.User.Username
                })
                .ToArrayAsync();
            return messages;
        }

        public async Task<Guid> CreateChat(InputChatDto inputChatDto, Guid adminId)
        {
            var users = inputChatDto.UserIds.Select(x => new User()
            {
                Id = x
            }).ToArray();

            _dbContext.AttachRange(users);
            
            var newChat = new Chat()
            {
                Title = inputChatDto.Title,
                UsersInChat = users,
                AdminId = adminId
            };
            _dbContext.Chats.Add(newChat);
            await _dbContext.SaveChangesAsync();
            return newChat.Id;
        }
    }
}
