using CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces;
using CB.BackDefault.Application.Aggregates.AuthAggregate.Services;
using CB.BackDefault.Application.AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CB.BackDefault.Application.Extensions
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IRefreshTokenService, RefreshTokenService>();

            return services;
        }
        public static IServiceCollection AddApplicationAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile).Assembly);

            return services;
        }
    }
}
