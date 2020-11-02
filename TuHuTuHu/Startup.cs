using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using TuHuTuHu.Hubs;

[assembly: OwinStartup(typeof(TuHuTuHu.Startup))]

namespace TuHuTuHu
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var idProvider = new CustomUserIdProvider();

            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);

            app.MapSignalR();
        }
    }
}
