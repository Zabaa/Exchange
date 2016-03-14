using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace Exchange.Hubs.Chat
{
    [Authorize]
    public class ChatMonitorHub : Hub
    {
        private readonly ChatMonitor _chatMonitor;

        public ChatMonitorHub() : this(ChatMonitor.Instance)
        {
            
        }

        public ChatMonitorHub(ChatMonitor chatMonitor)
        {
            _chatMonitor = chatMonitor;
        }


        public override Task OnConnected()
        {
            string userName = Context.User.Identity.Name;
            string userId = Context.User.Identity.GetUserId();

            _chatMonitor.AddContact(userName, userId, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            string userName = Context.User.Identity.Name;
            string userId = Context.User.Identity.GetUserId();

            _chatMonitor.AddContact(userName, userId, Context.ConnectionId);
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.User.Identity.Name;
            _chatMonitor.RemoveContact(userName, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }
}