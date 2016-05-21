using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Exchange.Domain.Account;
using Exchange.Domain.Chat;
using Exchange.ViewModel.Chat;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Exchange.Hubs.Chat
{
    public class ChatMonitor
    {
        private static readonly Lazy<ChatMonitor> _instance =
            new Lazy<ChatMonitor>(() => new ChatMonitor(GlobalHost.ConnectionManager.GetHubContext<ChatMonitorHub>().Clients));

        private IHubConnectionContext<dynamic> _clients;

        private readonly ConcurrentDictionary<string, string> _usersOnline = new ConcurrentDictionary<string, string>();
        private readonly object usersOnlineLock = new object();
        private volatile bool _updatingUsersOnlineList = false;


        public static ChatMonitor Instance
        {
            get { return _instance.Value; }
        }

        public ChatMonitor(IHubConnectionContext<dynamic> clients)
        {
            _clients = clients;
        }

        public void SendMessage(string senderId, string recipientd, MessageViewModel message)
        {
            var users = new List<string> { senderId, recipientd };
            _clients.Users(users).addMessage(message);
        }

        public void AddConversation(Conversation conversation)
        {
            var users = new List<string> { conversation.SenderId, conversation.RecipientId };
            _clients.Users(users).addConversation(conversation);
        }

        public void AddContact(string name, string id, string connectionId)
        {
            lock (usersOnlineLock)
            {
                if (_updatingUsersOnlineList) return;
                _updatingUsersOnlineList = true;

                _usersOnline.AddOrUpdate(name, id, (key, value) =>
                {
                    value = id;
                    return value;
                });

                _clients.AllExcept(connectionId).addContact(new { Id = id, Name = name });

                _updatingUsersOnlineList = false;
            }
        }

        public void RemoveContact(string name, string connectionId)
        {
            lock (usersOnlineLock)
            {
                if (_updatingUsersOnlineList) return;
                _updatingUsersOnlineList = true;

                string id;
                if (_usersOnline.TryRemove(name, out id))
                    _clients.AllExcept(connectionId).removeContact(new { Id = id, Name = name });

                _updatingUsersOnlineList = false;
            }
        }

        public IDictionary<string, string> GetOnlineUser()
        {
            return _usersOnline;
        }
    }
}