using System.Collections.Generic;
using System.Net;
using Discuss.Domain.Models.Entities;

namespace Discuss.Domain.Models.Contract.Result
{
    public class IdentityResult
    {
        
        public User User { get; set; }
        public bool Succeeded { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}