using System.Collections.Generic;
using System.Threading.Tasks;
using Discuss.Domain.Models.Contract.Result;
using Discuss.Domain.Models.Entities;

namespace Discuss.Domain.Interfaces
{
    public interface IFriendService
    {
        Task<Result> AddAsync(string userId,string friendId);
        IEnumerable<User> GetFriends(string userId);
        Dictionary<string,bool> GetFriendsWithStatus(string userId);
    }
}