using System.Collections.Generic;

namespace Discuss.Domain.Models.Contract.Result
{
    public class FriendResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}