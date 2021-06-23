using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Discuss.Persistence
{
    public static class DependencyInjection
    {
        public static string DbConnection = @"Server=(localdb)\MSSQLLocalDB;Database=aspnet-63bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(DbConnection));
            return services;
        }
    }
}


////MIGRACJA BAZY DANYCH
/// https://docs.microsoft.com/pl-pl/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli
/// INSTRUKCJA
/// Otwieramy CMD lub PowerShell
/// Wpisujemy cd i ścieżka do projektu
/// Następnie 
///dotnet ef migrations add NewMigration --project Discuss.Persistence
/// i na koniec
/// dotnet ef database update --project Discuss.Persistence