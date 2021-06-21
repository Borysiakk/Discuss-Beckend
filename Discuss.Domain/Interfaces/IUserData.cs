using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discuss.Domain.Models;

namespace Discuss.Domain.Interfaces
{
    public interface IUserData
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetUsersByLogin(string login);
        IEnumerable<User> GetUsersByEmail(string email);
        User GetById(long id);
        User Update(User user);
        User Add(User user);
        User Delete(long id);
        int GetUsersCount();
    }
}
