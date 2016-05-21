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
            return
                _exchangeContext.Conversations.Where(c => c.SenderId.Equals(userId) || c.RecipientId.Equals(userId))
                    .ToList();
        }

        public Conversation GetConversation(int id)
        {
            var conversation = _exchangeContext.Conversations.SingleOrDefault(c => c.Id == id);

            if (conversation != null && conversation.Sender == null)
                conversation.Sender = _exchangeContext.Users.SingleOrDefault(u => u.Id.Equals(conversation.SenderId));
            if(conversation != null && conversation.Recipient == null)
                conversation.Recipient = _exchangeContext.Users.SingleOrDefault(u => u.Id.Equals(conversation.RecipientId));

            return conversation;
        }

        public void AddConversation(Conversation conversation)
        {
            if (!_exchangeContext.Users.Any(u => u.Id.Equals(conversation.RecipientId)))
                return;

            bool conversationExist = _exchangeContext.Conversations.Any(c =>
                (c.RecipientId.Equals(conversation.RecipientId) && c.SenderId.Equals(conversation.SenderId))
                || (c.RecipientId.Equals(conversation.SenderId) && c.SenderId.Equals(conversation.RecipientId)));

            if (conversationExist)
                return;

            _exchangeContext.Conversations.Add(conversation);
            _exchangeContext.SaveChanges();
        }

        public void AddMessage(Message message)
        {
            _exchangeContext.Messages.Add(message);
            _exchangeContext.SaveChanges();
        }
    }
}
