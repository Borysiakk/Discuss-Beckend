using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Discuss.Domain.Models.Contract.Result;
using Discuss.Domain.Models.Entities;

namespace Discuss.Domain.Models
{
    public class AuthenticateResult :Result
    {
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime AuthDate { get; set; }
    }
}
