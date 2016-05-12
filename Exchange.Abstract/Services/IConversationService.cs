using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain.Chat;

namespace Exchange.Abstract.Services
{
    public interface IConversationService
    {
        IEnumerable<Conversation> GetConversations(string userId);
        Conversation GetConversation(int id);
        void AddConversation(Conversation conversation);
        void AddMessage(Message message);
    }
}
