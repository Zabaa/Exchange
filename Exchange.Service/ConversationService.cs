using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Abstract.Services;
using Exchange.DataAccess.Context;
using Exchange.Domain.Chat;

namespace Exchange.Service
{
    public class ConversationService : IConversationService
    {
        private readonly ExchangeContext _exchangeContext;

        public ConversationService()
        {
            _exchangeContext = new ExchangeContext();
        }

        public IEnumerable<Conversation> GetConversations(string userId)
        {
            return _exchangeContext.Conversations.Where(c => c.SenderId.Equals(userId) || c.RecipientId.Equals(userId));
        }
    }
}
