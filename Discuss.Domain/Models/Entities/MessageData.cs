using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuss.Domain.Models.Entities
{
    public class MessageData
    {
        public string Message { get; set; }
        public bool Send { get; set; }
        public DateTime Date { get; set; }
        public string Login { get; set; }
    }
}
