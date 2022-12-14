using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Create;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using FC.CodeFlix.Catalog.Infra.Data.EF;
using FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;
using MediatR;
namespace FC.CodeFlix.Catalog.Api.Configurations
{
    public static class UseCasesCofigiration
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateCategory));
            services.AddRepositories();

            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository,CategoryRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
          return services;
        }
    }
}
