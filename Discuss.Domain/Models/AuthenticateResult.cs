using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Discuss.Domain.Models.Entities;

namespace Discuss.Domain.Models
{
    public class AuthenticateResult
    {
        public string Token { get; set; }
        //public User User { get; set; }
        public bool Succeeded { get; set; }
        public DateTime AuthDate { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
