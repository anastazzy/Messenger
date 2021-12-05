using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain.Contracts
{
    public class OutputMessageDto
    {
        public string Text { get; set; }
        public Status Status { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public string Username { get; set; }
        public Guid ChatId { get; set; }
    }
}
