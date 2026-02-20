using CB.BackDefault.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace CB.BackDefault.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            // services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
