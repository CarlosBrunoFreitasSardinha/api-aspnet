using CB.BackDefault.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CB.BackDefault.Infra.Data.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BackDefaultContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<Func<BackDefaultContext>>(provider => () => provider.GetRequiredService<BackDefaultContext>());
            services.AddScoped<DbFactory>();
            // services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
