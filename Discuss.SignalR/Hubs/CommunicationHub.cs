using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discuss.Domain.Models.Entities;
using Discuss.SignalR.Models;
using Discuss.SignalR.Interface;

namespace Discuss.SignalR.Hubs
{
    public class CommunicationHub: Hub<IClientHub>
    {
        private ILogger<CommunicationHub> logger;
        public CommunicationHub(ILogger<CommunicationHub>logger)
        {
            this.logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var host = Context.GetHttpContext().Request.Headers["Origin"];
            logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} Host {host} dołączył do rozmowy.");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var host = Context.GetHttpContext().Request.Headers["Origin"];
            logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} Host {host} rozłączył się.");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageToClient(MessageData messageData)
        {
            try
            {
                string msgInfo = $"{messageData?.Date.ToString("yyyy-MM-dd hh:mm:ss")} {messageData?.SendingClientId}: {messageData.Message}";
                logger.LogInformation(msgInfo);
                await Clients.All.ReceiveMessageFromServer(messageData);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured: {ex.Message}");
            }
        }

        public async Task SendNotifyMessageToClient(string destinationClientId, string messageId)
        {
            try
            {
                string notifyInfo = $"Sending notify to client {destinationClientId} about message id: {messageId}.";
                logger.LogInformation(notifyInfo);
                await Clients.Client(destinationClientId).ReceiveNotifyFromServer(destinationClientId, messageId);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured: { ex.Message}");
            }
        }
    }
}
