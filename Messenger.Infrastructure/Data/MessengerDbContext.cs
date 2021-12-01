using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.Domain;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastructure.Data
{
    public class MessengerDbContext : DbContext
    {
        public MessengerDbContext(DbContextOptions<MessengerDbContext> options) : base (options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }


    }
}
