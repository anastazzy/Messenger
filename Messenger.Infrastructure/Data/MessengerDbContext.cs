using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique(); 
            
            modelBuilder.Entity<Chat>()
                .HasMany<User>(x => x.UsersInChat)
                .WithMany(x => x.UserChats);

            modelBuilder.Entity<Chat>()
                .HasOne<User>(x => x.Admin)
                .WithMany(x => x.UserChatAsAdmin);
        }
    }
}
