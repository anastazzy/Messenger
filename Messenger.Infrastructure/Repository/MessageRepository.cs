using System;
using System.Threading.Tasks;
using Messenger.Domain;
using Messenger.Domain.Contracts;
using Messenger.Infrastructure.Data;

namespace Messenger.Infrastructure.Repository
{
    public class MessageRepository
    {
        private readonly MessengerDbContext _dbContext;

        public MessageRepository(MessengerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
    }
}