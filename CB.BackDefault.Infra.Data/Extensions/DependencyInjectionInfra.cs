using CB.BackDefault.Domain.Aggregates.IdentityAggregate.Interfaces;
using CB.BackDefault.Domain.Shared.Interfaces;
using CB.BackDefault.Infra.Data.Aggregates.IdentityAggregate.Repositories;
using CB.BackDefault.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CB.BackDefault.Infra.Data.Extensions
{
    public static class DependencyInjectionInfra
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BackDefaultContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                                             b => b.MigrationsAssembly("CB.BackDefault.Infra.Data")));

            services.AddScoped<Func<BackDefaultContext>>(provider => () => provider.GetRequiredService<BackDefaultContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped(typeof(IRefreshTokenRepository), typeof(RefreshTokenRepository));
        }
    }
}
