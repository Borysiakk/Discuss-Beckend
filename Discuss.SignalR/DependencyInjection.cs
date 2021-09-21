using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discuss.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Discuss.SignalR
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSignalRHubs(this IServiceCollection services)
        {
            services.AddSignalR();
            return services;
        }

        public static IApplicationBuilder UseSignalR(this IApplicationBuilder app)
        {
            app.UseEndpoints(e => e.MapHub<CommunicationHub>("/CommunicationHub"));
            return app;
        }
    }
}
