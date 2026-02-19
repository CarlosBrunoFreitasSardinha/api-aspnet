using AutoMapper;
using CB.BackDefault.Application.AutoMapper;

namespace CB.BackDefault.Api.Configurations
{
    public static class AddAutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {

            var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile<AutoMapperProfile>();
            }).CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
