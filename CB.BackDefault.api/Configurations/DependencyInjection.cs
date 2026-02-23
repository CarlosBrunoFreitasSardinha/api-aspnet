using CB.BackDefault.Api.Extensions;
using CB.BackDefault.Application.Extensions;
using CB.BackDefault.Application.Shared.Settings;
using CB.BackDefault.Infra.Data.Extensions;


namespace CB.BackDefault.Api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddSingleton(jwtSettings);

            services.AddApplication();
            services.AddInfra(configuration);
            services.AddAuthConfiguration(configuration);
            return services;
        }

    }
}
