using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain
{
    public class InputChatDto
    {
        public string Title { get; init; }
        public IEnumerable<Guid> UserIds { get; init; }
        
    }
}
