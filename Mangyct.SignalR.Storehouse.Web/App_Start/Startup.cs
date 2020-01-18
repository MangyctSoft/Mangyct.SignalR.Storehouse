using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Mangyct.SignalR.Storehouse.Web.App_Start.Startup))]

namespace Mangyct.SignalR.Storehouse.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Дополнительные сведения о настройке приложения см. на странице https://go.microsoft.com/fwlink/?LinkID=316888

            app.MapSignalR();
        }
    }
}
