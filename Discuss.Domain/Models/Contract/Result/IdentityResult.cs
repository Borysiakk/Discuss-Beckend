using System.Collections.Generic;
using System.Net;
using Discuss.Domain.Models.Entities;

namespace Discuss.Domain.Models.Contract.Result
{
    public class IdentityResult :Result
    {
        public User User { get; set; }
    }
}