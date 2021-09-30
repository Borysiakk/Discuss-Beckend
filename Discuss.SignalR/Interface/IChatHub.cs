using Discuss.SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuss.SignalR.Interface
{
    public interface IChatHub
    {
        Task RecieveMessageFromServer(MessageData messageData);

        Task RecieveNotifyFromServer(string destinationClientId, string messageId);
    }
}
