using System.Collections.Generic;
using System.Linq;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models.Entities;
using Discuss.Persistence;

namespace Discuss.Infrastructure.Services
{
    public class FriendService :IFriendService
    {
        private readonly ApplicationDbContext _dbContext;

        public FriendService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}