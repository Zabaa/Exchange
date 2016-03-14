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
            var user = Context.User;
            Clients.Others.addContact(new { Id = user.Identity.GetUserId(), Name = user.Identity.Name });
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            //TODO: chat log again
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            //TODO: chat logout
            return base.OnDisconnected(stopCalled);
        }
    }
}