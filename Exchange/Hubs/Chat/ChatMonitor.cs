using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Exchange.Hubs.Chat
{
    public class ChatMonitor
    {
        private static readonly Lazy<ChatMonitor> _instance =
            new Lazy<ChatMonitor>(
                () => new ChatMonitor(GlobalHost.ConnectionManager.GetHubContext<ChatMonitorHub>().Clients));

        private IHubConnectionContext<dynamic> _clients;

        public static ChatMonitor Instance
        {
            get { return _instance.Value; }
        }

        public ChatMonitor(IHubConnectionContext<dynamic> clients)
        {
            _clients = clients;
        }

        public void SendMessage(string userId, string message)
        {
            _clients.User(userId).addMessage(message);
        }
    }
}