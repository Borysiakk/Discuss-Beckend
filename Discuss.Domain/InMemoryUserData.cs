using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models;

namespace Discuss.Domain
{
    //Klasa mockupowa do testów
    public class InMemoryUserData : IUserService
    {
        List<User> Users;

        public InMemoryUserData()
        {
            Users = new List<User>()
            {
                new User { Id = 1, Login = "jnowak", Email = "jnowak@test.pl", HashPass = "*******" },
                new User { Id = 2, Login = "jkowalski", Email = "jkowalski@test.pl", HashPass = "***" }
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

        public async Task<User> AddAsync(User user)
        {
            await Task.Delay(100);
            long currentId = Users.Max(u => u.Id);
            user.Id = currentId + 1;
            Users.Add(user);
            return user;
        }

        public async Task<User> DeleteAsync(long id)
        {
            await Task.Delay(100);
            User user = Users.Find(u => u.Id == id);
            if (user != null)
            {
                Users.Remove(user);
                return user;
            }
            else
                return null;
        }

        public async Task<User> UpdateAsync(User user)
        {
            await Task.Delay(100);
            int idx = Users.FindIndex(u => u.Id == user.Id);
            if (idx != -1)
            {
                Users[idx] = user;
                return Users[idx];
            }
            else
                return null;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            await Task.Delay(100);
            return Users;
        }

        public async Task<IEnumerable<User>> GetUsersByLoginAsync(string login)
        {
            await Task.Delay(100);
            return Users.FindAll(u => u.Email.Contains(login));
        }

        public async Task<IEnumerable<User>> GetUsersByEmailAsync(string email)
        {
            await Task.Delay(100);
            return Users.FindAll(u => u.Email.Contains(email));
        }
    }
}
