using FC.CodeFlix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.Api.Configurations
{
    public static class ConnectionsConfiguration
    {
        public static IServiceCollection AddAppConnections(this IServiceCollection services)
        {
            services.AddDbConection();
            return services;
        }
        private static IServiceCollection AddDbConection(this IServiceCollection services)
        {
            services.AddDbContext<CodeFlixCatalogDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemory-DSV-Database");
            });
            return services;
        }
    }
}
