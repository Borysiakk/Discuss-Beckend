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
    public class InMemoryUserData : IUserData
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

        public User Add(User user)
        {
            long currentId = Users.Max(u => u.Id);
            user.Id = currentId + 1;
            Users.Add(user);
            return user;
        }

        public User Update(User user)
        {
            int idx = Users.FindIndex(u => u.Id == user.Id);
            if (idx != -1)
            {
                Users[idx] = user;
                return Users[idx];
            }
            else
                return null;
        }

        public User Delete(long id)
        {
            User user = Users.Find(u => u.Id == id);
            if (user != null)
            {
                Users.Remove(user);
                return user;
            }
            else
                return null;
        }

        public User GetById(long id)
        {
            return Users.Find(u => u.Id == id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Users;
        }

        public IEnumerable<User> GetUsersByEmail(string email)
        {
            return Users.FindAll(u => u.Email.Contains(email));
        }

        public IEnumerable<User> GetUsersByLogin(string login)
        {
            return Users.FindAll(u => u.Email.Contains(login));
        }

        public int GetUsersCount()
        {
            return Users.Count;
        }
    }
}
