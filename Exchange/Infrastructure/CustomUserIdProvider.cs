using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Exchange.DataAccess.Context;
using Exchange.Domain.Account;
using Microsoft.AspNet.SignalR;

namespace Exchange.Infrastructure
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        private readonly ExchangeContext _exchangeContext;

        public CustomUserIdProvider()
        {
            _exchangeContext = new ExchangeContext();
        }

        public string GetUserId(IRequest request)
        {
            string userName =  request.User.Identity.Name;
            var user = _exchangeContext.Users.SingleOrDefault(u => u.UserName.Equals(userName));
            return user != null ? user.Id : string.Empty;
        }
    }
}