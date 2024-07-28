using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WABU.Hubs
{
    [HubName("signalRNotificationHub")]
    public class SignalRNotificationHub : Hub
    {
        private static string conString = ConfigurationManager.ConnectionStrings["FWDbContext"].ToString();

        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRNotificationHub>();
            context.Clients.All.updateMessages();
        }
    }
}