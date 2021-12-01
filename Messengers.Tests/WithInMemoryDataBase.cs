using System;
using Messenger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Messengers.Tests
{
    public class WithInMemoryDataBase
    {
        public MessengerDbContext DbContext { get; }
        protected WithInMemoryDataBase()
        {
            var options = new DbContextOptionsBuilder<MessengerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            DbContext = new MessengerDbContext(options);
            DbContext.Database.EnsureCreated();
        }

    }
}
