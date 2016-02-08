using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.ViewModel.Chat
{
    public class ChatViewModel
    {
        public IEnumerable<ConversationViewModel> Conversations { get; set; }
        public string CurrentUserId { get; set; }
    }
}
