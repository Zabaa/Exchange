using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Exchange.DataAccess.Context;
using Exchange.Domain.Account;
using Exchange.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Exchange.Infrastructure
{
    public class DbInitializer : CreateDatabaseIfNotExists<ExchangeContext>
    {
        protected override void Seed(ExchangeContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string name = "Admin";
            string password = "ZAQ!2wsx";


            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new IdentityRole(name));
            }

            var user = new ApplicationUser();
            user.UserName = name;
            var adminresult = UserManager.Create(user, password);

            ////Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }
            base.Seed(context);
        }
    }
}