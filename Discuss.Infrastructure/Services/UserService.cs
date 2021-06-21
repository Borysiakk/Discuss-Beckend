using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models;
using Discuss.Persistence;

namespace Discuss.Infrastructure.Services
{
    public class UserService :IUserService
    {
        private readonly ApplicationDbContext _dbContex;

        public UserService(ApplicationDbContext dbContex)
        {
            _dbContex = dbContex;
        }

        public int GetUsersCount()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByIdAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> AddAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> DeleteAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> UpdateAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetUsersByLogin(string login)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetUsersByEmail(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}