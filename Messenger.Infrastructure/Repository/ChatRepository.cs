using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.Infrastructure.Data;
using Messenger.Domain.Contracts;

namespace Messenger.Infrastructure.Repository
{
    public class ChatRepository
    {
        private readonly MessengerDbContext _dbContext;

        public ChatRepository(MessengerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
    }
}
