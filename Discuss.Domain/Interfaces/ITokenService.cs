using Discuss.Domain.Models.Entities;

namespace Discuss.Domain.Interfaces
{
    public interface ITokenService
    {
        public string Generate(User user);
    }
}