using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.ViewModel.Chat
{
    public class ConversationViewModel
    {
        public int Id { get; set; }
        public string RecipientId { get; set; }
        public string RecipientName { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }

        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}
