using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exchange.Startup))]
namespace Exchange
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
