using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.ViewModel.Chat
{
    public class MessageViewModel
    {
        public int? Id { get; set; }
        [StringLength(512)]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int ConversationId { get; set; }
        public bool IsSender { get; set; }
    }
}
