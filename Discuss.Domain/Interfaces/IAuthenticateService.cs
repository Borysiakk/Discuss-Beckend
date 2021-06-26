using System.Threading.Tasks;
using Discuss.Domain.Models;
using Discuss.Domain.Models.Contract;
using Discuss.Domain.Models.Contract.Result;

namespace Discuss.Domain.Interfaces
{
    public interface IAuthenticateService
    {
        public Task<AuthenticateResult> LoginAsync(LoginModelView loginModelView);
        public Task<AuthenticateResult> RegisterAsync(RegisterViewModel registerViewModel);
    }
}