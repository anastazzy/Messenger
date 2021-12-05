using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain
{
    public class ListOfChatDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastMessage { get; set; }
        public string Username { get; set; }
        public Status Status { get; set; }
    }
}
