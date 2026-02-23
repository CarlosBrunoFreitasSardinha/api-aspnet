using CB.BackDefault.Infra.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace CB.BackDefault.IntegrationTests.Base
{
    public class IntegrationTestBase : WebApplicationFactory<Program>
    {
        private static readonly object _dbLock = new object();
        private static bool _databaseInitialized = false;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.Sources.Clear();
                config.AddJsonFile("appsettings.json", optional: false);
                config.AddEnvironmentVariables();
                config.AddUserSecrets<IntegrationTestBase>();
            });

            builder.ConfigureServices((context, services) =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<BackDefaultContext>));
                if (descriptor != null) services.Remove(descriptor);

                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                // garante a existencia de um banco fisico uma unica vez por execucao
                lock (_dbLock)
                {
                    if (!_databaseInitialized)
                    {
                        EnsureDatabaseExists(connectionString!);
                        _databaseInitialized = true;
                    }
                }

                services.AddDbContext<BackDefaultContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<BackDefaultContext>();

                db.Database.EnsureCreated();//sem delete, para garantir que um teste não exclua o banco do outro
            });
        }

        private void EnsureDatabaseExists(string connectionString)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            var databaseName = builder.Database;
            builder.Database = "postgres";

            using var connection = new NpgsqlConnection(builder.ToString());
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'";
            var exists = command.ExecuteScalar() != null;

            if (!exists)
            {
                command.CommandText = $"CREATE DATABASE \"{databaseName}\"";
                command.ExecuteNonQuery();
            }
        }
    }
}