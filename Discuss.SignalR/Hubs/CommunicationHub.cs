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
using Discuss.Domain.Interfaces;

namespace Discuss.SignalR.Hubs
{
    public class CommunicationHub: Hub<IClientHub>
    {
        private ILogger<CommunicationHub> _logger;
        private IUserService _userService;
        public CommunicationHub(ILogger<CommunicationHub>logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var id = Context.GetHttpContext().Request.Headers["Id"].ToString();
                var host = Context.GetHttpContext().Request.Headers["Origin"];
                var userName = Context.GetHttpContext().Request.Headers["UserName"].ToString();
                var connectingUser = await _userService.GetUserByLoginAsync(userName);
                if(connectingUser != null)
                {
                    await _userService.AddClientCommunicationHubIdAsync(connectingUser, id);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} Host {host} dołączył do rozmowy.");
                await base.OnConnectedAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error occured: {ex.Message}");
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var host = Context.GetHttpContext().Request.Headers["Origin"];
                var userName = Context.GetHttpContext().Request.Headers["UserName"].ToString();
                var connectingUser = await _userService.GetUserByLoginAsync(userName);
                if (connectingUser != null)
                {
                    await _userService.RemoveClientCommunicationHubIdAsync(connectingUser);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} Host {host} rozłączył się.");

                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured: {ex.Message}");
            }
        }

        public async Task SendMessageToClient(MessageData messageData)
        {
            try
            {
                string msgInfo = $"{messageData?.Date.ToString("yyyy-MM-dd hh:mm:ss")} {messageData?.SendingClientId}: {messageData.Message}";
                _logger.LogInformation(msgInfo);
                await Clients.All.ReceiveMessageFromServer(messageData);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured: {ex.Message}");
            }
        }

        public async Task SendNotifyMessageToClient(string destinationClientId, string messageId)
        {
            try
            {
                string notifyInfo = $"Sending notify to client {destinationClientId} about message id: {messageId}.";
                _logger.LogInformation(notifyInfo);
                await Clients.Client(destinationClientId).ReceiveNotifyFromServer(destinationClientId, messageId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured: { ex.Message}");
            }
        }
    }
}
