using FC.CodeFlix.Catalog.Infra.Data.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace FC.CodeFlix.Catalog.End2EndTests.Common
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("EndToEndTest");
            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetService<CodeFlixCatalogDbContext>();
                ArgumentNullException.ThrowIfNull(context);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            });
            base.ConfigureWebHost(builder);
        }
    }
}
