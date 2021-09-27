using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discuss.Domain.Models.Entities;

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
            var host = Context.GetHttpContext().Request.Headers["Origin"];
            logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} Host {host} dołączył do rozmowy.");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var host = Context.GetHttpContext().Request.Headers["Origin"];
            logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} Host {host} rozłączył się.");
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(MessageData messageData)
        {
            try
            {
                string msgInfo = $"{messageData?.Date.ToString("yyyy-MM-dd hh:mm:ss")} {messageData?.Login}: {messageData.Message}";
                await Clients.All.SendAsync("BroadcastMessage", msgInfo);
            }
            catch(Exception ex)
            {
                logger.LogError($"Error occured: {ex.Message}");
            }
        }
    }
}
