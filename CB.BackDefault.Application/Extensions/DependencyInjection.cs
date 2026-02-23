using CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces;
using CB.BackDefault.Application.Aggregates.AuthAggregate.Services;
using CB.BackDefault.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace CB.BackDefault.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
