using Microsoft.AspNet.SignalR;

namespace Mangyct.SignalR.Storehouse.Web.Hubs
{
    public class StorehouseHub : Hub
    {
        public void Show()
        {
            Clients.All.displayStorehouse();
        }
    }
}