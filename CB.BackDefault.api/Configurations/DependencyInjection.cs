namespace CB.BackDefault.Api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            //services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
