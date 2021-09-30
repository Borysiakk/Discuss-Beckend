
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models.Contract.Result;
using Discuss.Domain.Models.Entities;
using Discuss.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Discuss.Infrastructure.Services
{
    public class FriendService : IFriendService
    {
        private readonly ApplicationDbContext _dbContext;

        public FriendService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result> AddAsync(string userId, string friendId)
        {
            var friends = _dbContext.Friends.Where(a => a.UserId == userId);
            if (friends.Any())
            {
                var isFriend = await friends.FirstOrDefaultAsync(a => a.FriendlyId == friendId);
                if (isFriend != null)
                {
                    return new Result()
                    {
                        Succeeded = false,
                        StatusCode = HttpStatusCode.Conflict,
                        Errors = new[] { "You have this friend in friends " }
                    };
                }
            }

            Friend friend = new Friend()
            {
                UserId = userId,
                FriendlyId = friendId
            };

            await _dbContext.Friends.AddAsync(friend);
            await _dbContext.SaveChangesAsync();

            return new Result()
            {
                Succeeded = true,
                StatusCode = HttpStatusCode.OK,
            };
        }

        public IEnumerable<User> GetFriends(string userId)
        {
            return _dbContext.Friends.Where(a => a.UserId == userId).Select(i => i.Friendly);
        }

        public Dictionary<string, bool> GetFriendsWithStatus(string userId)
        {
            return _dbContext.Friends.Where(a => a.UserId == userId).Select(b => b.Friendly).ToDictionary(c => c.Id, c => c.Status);
        }
    }
}