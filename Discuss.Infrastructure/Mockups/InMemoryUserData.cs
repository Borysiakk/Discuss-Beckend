using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models;
using Discuss.Domain.Models.Contract.Result;
using Discuss.Domain.Models.Entities;
using Microsoft.Extensions.Logging;

namespace Discuss.Domain
{
    //Klasa mockupowa do testów
    public class InMemoryUserData : IUserService
    {
        List<User> Users;
        private ILogger<InMemoryUserData> logger;
        public InMemoryUserData(ILogger<InMemoryUserData>logger)
        {
            this.logger = logger;
            Users = new List<User>()
            {
                new User { Id = 1, Login = "jnowak", Email = "jnowak@test.pl" },
                new User { Id = 2, Login = "jkowalski", Email = "jkowalski@test.pl" }
            };
        }

        public int GetUsersCount()
        {
            return Users.Count;
        }

        public async Task<User> GetByIdAsync(long id)
        {
            await Task.Delay(100);
            return Users.Find(u => u.Id == id);
        }

        // public async Task<IdentityResult> CreateAsync(User user,string password)
        // {
        //     await Task.Delay(100);
        //     long currentId = Users.Max(u => u.Id);
        //     user.Id = currentId + 1;
        //     Users.Add(user);
        //     logger.LogInformation($"User {user.Id} was added...");
        //     return user;
        // }

        public async Task<User> DeleteAsync(long id)
        {
            await Task.Delay(100);
            User user = Users.Find(u => u.Id == id);
            if (user != null)
            {
                Users.Remove(user);
                logger.LogInformation($"User {user.Id} removed...");
                return user;
            }
            else
            {
                logger.LogError($"User {id} not exist!");
                return null;
            }
        }

        public async Task<User> UpdateAsync(User user)
        {
            await Task.Delay(100);
            int idx = Users.FindIndex(u => u.Id == user.Id);
            if (idx != -1)
            {
                Users[idx] = user;
                logger.LogInformation($"User {user.Id} removed...");
                return Users[idx];
            }
            else
            {
                logger.LogError($"User {user.Id} not exist!");
                return null;
            }
        }

        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            await Task.Delay(100);
            logger.LogInformation("Users retrieved...");
            return Users;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            await Task.Delay(100);
            logger.LogInformation("Users by email retrieved...");
            return Users.FirstOrDefault(u=>u.Email==email);
        }

        public Task<User> GetUserByLoginAsync(string login)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetUsersByLoginAsync(string login)
        {
             await Task.Delay(100);
             logger.LogInformation("Users by login retrieved...");
             return Users.FindAll(u => u.Email.Contains(login));
        }
    }
}
