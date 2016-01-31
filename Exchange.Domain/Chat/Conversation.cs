using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain.Account;

namespace Exchange.Domain.Chat
{
    public class Conversation : BaseEntity
    {
        public string RecipientId { get; set; }
        public virtual ApplicationUser Recipient { get; set; }

        public string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
