using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
      //  public DateTimeOffset CreationTime { get; set; }

        public IEnumerable<Chat> UserChats { get; set; }
        public IEnumerable<Chat> UserChatAsAdmin { get; set; }
        public IEnumerable<Message> UserMessages { get; set; }
    }
}
