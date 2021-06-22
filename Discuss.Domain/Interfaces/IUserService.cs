using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discuss.Domain.Models;

namespace Discuss.Domain.Interfaces
{
    public interface IUserService
    {
        int GetUsersCount();
        Task<User> GetByIdAsync(long id);
        Task<User> AddAsync(User user);
        Task<User> DeleteAsync(long id);
        Task<User> UpdateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetUsersByLoginAsync(string login);
        Task<IEnumerable<User>> GetUsersByEmailAsync(string email);
    }
}
