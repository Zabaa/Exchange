using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IDictionary<string, string> GetOnlineUsers()
        {
            return
                (IDictionary<string, string>)
                    _chatMonitor.GetOnlineUser().Where(u => !u.Key.Equals(Context.User.Identity.Name));
        } 

        public override Task OnConnected()
        {
            AddOrUpdateContact();
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            AddOrUpdateContact();
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.User.Identity.Name;
            _chatMonitor.RemoveContact(userName, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
        
        private void AddOrUpdateContact()
        {
            string userName = Context.User.Identity.Name;
            string userId = Context.User.Identity.GetUserId();

            _chatMonitor.AddContact(userName, userId, Context.ConnectionId);
        }
    }
}