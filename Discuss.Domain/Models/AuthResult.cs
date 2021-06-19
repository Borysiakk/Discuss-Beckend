using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace Discuss.Domain.Models
{
    public class AuthResult
    {
        public string Token { get; set; }
        public User User { get; set; }
        public DateTime AuthDate { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
