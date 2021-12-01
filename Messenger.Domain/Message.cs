using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain
{
    public class Message
    {
        public  Guid Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public Status Status { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }

        public User User { get; set; }
        public Chat Chat { get; set; }
    }
}
