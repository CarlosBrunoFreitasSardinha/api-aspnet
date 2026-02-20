using CB.BackDefault.Application.Extensions;
using CB.BackDefault.Infra.Data.Extensions;


namespace CB.BackDefault.Api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddInfra(configuration);
            return services;
        }
    }
}
