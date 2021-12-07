using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.Domain;
using Messenger.Infrastructure.Data;
using Messenger.Domain;
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
        public async Task<ListOfChatDto[]> GetListOfChats(Guid userId)
        {
            var response = await _dbContext.Chats
                .Where(x => x.UsersInChat.Select(y => y.Id).Contains(userId))
                .Include(x => x.MessagesInChat)
                .ThenInclude(x => x.User)
                .Select(x => new 
                {
                    x.Id,
                    x.Title,
                    LastMessage = x.MessagesInChat.OrderBy(x => x.CreationTime).Last()
                })
                .ToArrayAsync();

            return response.Select(x => new ListOfChatDto()
            {
                LastMessage = x.LastMessage.Text,
                Name = x.Title,
                Status = x.LastMessage.Status,
                Username = x.LastMessage.User.Username,
                Id = x.Id
            }).ToArray();
        }
        
        public Task<OutputMessageDto[]> GetChatMessages(Guid id)
        {
            var messages =  _dbContext.Messages
                .Where(x => x.ChatId == id)
                .OrderBy(x => x.CreationTime)
                .Select(x => new OutputMessageDto()
                {
                    CreationTime = x.CreationTime,
                    Status = x.Status,
                    Text = x.Text,
                    Username = x.User.Username,
                    ChatId = x.ChatId
                })
                .ToArrayAsync();
            return messages;
        }
        
        public async Task<Guid> CreateMessageAsync(InputMessageDto inputMessageDto, Guid userId, Guid chatId)
        {
            var message = new Message()
            {
                Text = inputMessageDto.Text,
                UserId = userId,
                ChatId = chatId,
                Status = Status.Delivered,
            };
            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();
            return message.Id;
        }

        
    }
}
