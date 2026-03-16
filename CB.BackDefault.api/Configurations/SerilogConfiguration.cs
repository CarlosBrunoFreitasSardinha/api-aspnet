using Serilog;

namespace CB.BackDefault.Api.Configurations
{
    public static class SerilogConfiguration
    {

        public static IHostBuilder AddSerilogConfiguration(this IHostBuilder host)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    "logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30)//prazo de retenção
                .CreateLogger();

            host.UseSerilog();

            return host;
        }
    }
}
