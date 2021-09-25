
using System.Collections.Generic;
using System.Linq;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models.Entities;
using Discuss.Persistence;

namespace Discuss.Infrastructure.Services
{
    public class FriendService : IFriendService
    {
        private readonly ApplicationDbContext _dbContext;

        public FriendService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetFriends(string userId)
        {
            return _dbContext.Friends.Where(a=>a.UserId == userId).Select(i => new User()
            {
                Id = i.Friendly.Id,
                Email = i.Friendly.Email,
                Login = i.Friendly.Login,
                Status = i.Friendly.Status,
            });
        }

        public Dictionary<string, bool> GetFriendsWithStatus(string userId)
        {
            return _dbContext.Friends.Where(a => a.UserId == userId).Select(b => b.Friendly).ToDictionary(c => c.Id, c => c.Status);
        }
    }
}