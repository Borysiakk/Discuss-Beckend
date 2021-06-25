using Discuss.Domain.Models.Contract;

namespace Discuss.Domain.Interfaces
{
    public interface IAuthenticateService
    {
        public void LoginAsync(LoginModelView loginModelView);
        public void RegisterAsync(RegisterViewModel registerViewModel);
    }
}