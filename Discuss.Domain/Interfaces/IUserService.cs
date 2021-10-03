using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discuss.Domain.Models;
using Discuss.Domain.Models.Contract.Result;
using Discuss.Domain.Models.Entities;

namespace Discuss.Domain.Interfaces
{
    public interface IUserService
    {
        int GetUsersCount();
        Task<User> GetByIdAsync(string id);
        Task<User> DeleteAsync(string id);
        Task<User> UpdateAsync(User user);
        Task<IdentityResult> CreateAsync(User user,string password);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByLoginAsync(string login);
        Task<IEnumerable<User>> GetUsersByLoginAsync(string login);
        Task<bool> AddClientCommunicationHubIdAsync(User user, string communicationId);
        Task<bool> RemoveClientCommunicationHubIdAsync(User user);
    }
}
