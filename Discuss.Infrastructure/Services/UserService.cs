using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discuss.Domain.Interfaces;
using Discuss.Domain.Models.Contract.Result;
using Discuss.Domain.Models.Entities;
using Discuss.Persistence;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Discuss.Infrastructure.Services
{
    public class UserService :IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetUsersCount()
        {
            return _dbContext.Users.Count();
        }

        public Task<User> GetByIdAsync(string id)
        {
            return _dbContext.Users.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IdentityResult> CreateAsync(User user,string password)
        {
            var resultEmail = await GetUserByEmailAsync(user.Email);
            if (resultEmail != null)
            {
                return new IdentityResult()
                {
                    Succeeded = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new [] {"There is already an account registered to this email"},
                };
            }

            var resultLogin = await GetUserByLoginAsync(user.Login);
            if (resultLogin != null)
            {
                return new IdentityResult()
                {
                    Succeeded = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new[] {"There is already an account registered to this login"}
                };
            }

            user.PasswordSalt = BC.GenerateSalt();
            user.PasswordHash = BC.HashPassword(password, user.PasswordSalt);
            
            var resultUser = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return new IdentityResult()
            {
                Succeeded = true,
                User = resultUser.Entity,
                StatusCode = HttpStatusCode.Created,
            };
        }

        public async Task<User> DeleteAsync(string id)
        {
            var user = _dbContext.Users.FirstOrDefault(a => a.Id == id);
            if (user == null)
            {
                return null;
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            if (await _dbContext.Users.FirstOrDefaultAsync(a => a.Id == user.Id) == null)
            {
                return null;
            }
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(a => a.Login == login);
        }

        public async Task<IEnumerable<User>> GetUsersByLoginAsync(string login)
        {
            var users = await _dbContext.Users.Where(a => a.Login.Contains(login)).ToListAsync();
            return users.AsEnumerable<User>();
        }
    }
}