using Discuss.Domain.Interfaces;
using Discuss.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Discuss.Infrastructure
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            
            return services;
        }
    }
}