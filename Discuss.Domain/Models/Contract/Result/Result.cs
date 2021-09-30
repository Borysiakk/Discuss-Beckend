using System.Collections.Generic;
using System.Net;

namespace Discuss.Domain.Models.Contract.Result
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}