using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuss.SignalR.Hubs
{
    public class CommunicationHub: Hub
    {
        private ILogger<CommunicationHub> logger;
        public CommunicationHub(ILogger<CommunicationHub>logger)
        {
            this.logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            logger.LogInformation($"Wysyłanie wiadomości do klientów: {message}");
            await Clients.All.SendAsync("BroadcastMessage", message);
        }
    }
}
