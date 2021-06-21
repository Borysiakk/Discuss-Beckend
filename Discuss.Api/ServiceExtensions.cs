using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discuss.Domain;
using Discuss.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Discuss.Api
{
    public static class ServiceExtensions
    {
        public static void ConfigureUserService(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, InMemoryUserData>(); //Mockup do podmiany na UserService
        }
    }
}
