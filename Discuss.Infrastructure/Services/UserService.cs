using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models.Entities;
using Discuss.Persistence;
using Microsoft.EntityFrameworkCore;

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
            return _dbContex.Users.Count();
        }

        public Task<User> GetByIdAsync(long id)
        {
            return _dbContex.Users.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<User> AddAsync(User user)
        {
            var result = await _dbContex.Users.AddAsync(user);
            await _dbContex.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<User> DeleteAsync(long id)
        {
            var user = _dbContex.Users.FirstOrDefault(a => a.Id == id);
            if (user == null)
            {
                return null;
            }

            _dbContex.Users.Remove(user);
            await _dbContex.SaveChangesAsync();
            
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            if (await _dbContex.Users.FirstOrDefaultAsync(a => a.Id == user.Id) == null)
            {
                return null;
            }
            _dbContex.Users.Update(user);
            await _dbContex.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _dbContex.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContex.Users.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsersByLoginAsync(string login)
        {
            var users = await _dbContex.Users.Where(a => a.Login.Contains(login)).ToListAsync();
            return users.AsEnumerable<User>();
        }
    }
}