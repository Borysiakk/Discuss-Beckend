using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuss.SignalR.Models
{
    public class MessageData
    {
        public string MessageId { get; set; }
        public string DestinationClientId { get; set; }
        public string SendingClientId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
