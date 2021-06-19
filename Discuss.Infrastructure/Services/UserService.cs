using System.Collections.Generic;
using System.Net.Mime;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models;
using Discuss.Persistence;

namespace Discuss.Infrastructure.Services
{
    public class UserService :IUserData
    {
        private readonly ApplicationDbContext _dbContex;

        public UserService(ApplicationDbContext dbContex)
        {
            _dbContex = dbContex;
        }

        public IEnumerable<User> GetUsersByLogin(string login)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetUsersByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public User GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        public User Update(User user)
        {
            throw new System.NotImplementedException();
        }

        public User Add(User user)
        {
            throw new System.NotImplementedException();
        }

        public User Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public int GetUsersCount()
        {
            throw new System.NotImplementedException();
        }
    }
}