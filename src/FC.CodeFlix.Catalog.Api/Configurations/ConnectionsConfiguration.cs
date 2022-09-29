using FC.CodeFlix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.Api.Configurations
{
    public static class ConnectionsConfiguration
    {
        public static IServiceCollection AddAppConnections(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbConection(configuration);
            return services;
        }
        private static IServiceCollection AddDbConection(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CatalogDB");
            services.AddDbContext<CodeFlixCatalogDbContext>(options =>
            {
                options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString));
            });
            return services;
        }
    }
}
