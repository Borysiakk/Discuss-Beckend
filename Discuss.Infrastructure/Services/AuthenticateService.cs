using Discuss.Domain.Interfaces;
using Discuss.Domain.Models.Contract;
using Discuss.Persistence;

namespace Discuss.Infrastructure.Services
{
    public class AuthenticateService :IAuthenticateService
    {
        private readonly ApplicationDbContext _dbContex;

        public AuthenticateService(ApplicationDbContext dbContex)
        {
            _dbContex = dbContex;
        }

        public void LoginAsync(LoginModelView loginModelView)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterAsync(RegisterViewModel registerViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}