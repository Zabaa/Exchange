using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
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
    }
}