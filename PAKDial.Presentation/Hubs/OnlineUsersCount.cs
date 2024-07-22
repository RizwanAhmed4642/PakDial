using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Hubs
{
    public class OnlineUsersCount : Hub
    {
        private static int Counts;

        public override Task OnConnectedAsync()
        {
            Counts++;
            base.OnConnectedAsync();
            this.Clients.All.SendAsync("updateCount", Counts);
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Counts--;
            base.OnDisconnectedAsync(exception);
            this.Clients.All.SendAsync("updateCount", Counts);
            return Task.CompletedTask;
        }
    }
}
