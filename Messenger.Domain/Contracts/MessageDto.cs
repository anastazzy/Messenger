using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain.Contracts
{
    public class MessageDto
    {
        public string Text { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public Status Status { get; set; }
        public string Username { get; set; }
    }
}
