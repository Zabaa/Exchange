using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Chat
{
    public class Message : BaseEntity
    {
        public int ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
