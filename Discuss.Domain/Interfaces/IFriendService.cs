using System.Collections.Generic;
using Discuss.Domain.Models.Entities;

namespace Discuss.Domain.Interfaces
{
    public interface IFriendService
    {
        IEnumerable<string> GetFriends(string userId);
        Dictionary<string,bool> GetFriendsWithStatus(string userId);
    }
}