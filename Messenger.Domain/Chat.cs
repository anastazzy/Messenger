using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public Guid AdminId { get; set; }
        public IEnumerable<User> UsersInChat { get; set; }
        public IEnumerable<Message> MessagesInChat { get; set; }

    }
}
