using Bogus;
using CB.BackDefault.Application.Aggregates.IdentityAggregate.ViewModels;
using CB.BackDefault.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CB.BackDefault.IntegrationTests.Base
{
    public class IntegrationTestsFixture<TProgram> : IDisposable
        where TProgram : class
    {
        public HttpClient Client { get; }
        public BackDefaultFactory<TProgram> Factory { get; }

        private readonly Faker _faker;

        public IntegrationTestsFixture()
        {
            Factory = new BackDefaultFactory<TProgram>();

            Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("http://localhost")
            });

            _faker = new Faker("pt_BR");
        }

        // 🔹 Gera dados fake SEM armazenar estado
        public (string Email, string Password) GerarUsuarioCredenciais()
        {
            var email = _faker.Internet.Email().ToLower();
            var senha = _faker.Internet.Password(8, false, "", "@1Ab_");

            return (email, senha);
        }

        // 🔹 Cria usuário via API (fluxo real)
        public async Task CriarUsuarioAsync(string email, string senha)
        {
            var registerViewModel = new
            {
                Email = email,
                Password = senha,
                ConfirmPassword = senha
            };

            var response = await Client.PostAsJsonAsync("api/auth/register", registerViewModel);
            response.EnsureSuccessStatusCode();
        }

        // 🔹 Obtém token SEM guardar em variável global
        public async Task<string> ObterTokenAsync(string email, string senha)
        {
            var loginViewModel = new LoginViewModel
            {
                Email = email,
                Password = senha
            };

            var response = await Client.PostAsJsonAsync("api/auth/login", loginViewModel);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResult>();

            return result!.Token;
        }
        private class LoginResult
        {
            public string Token { get; set; } = "";
        }

        // 🔹 Cria client autenticado isolado
        public async Task<HttpClient> CriarClientAutenticadoAsync()
        {
            var (email, senha) = GerarUsuarioCredenciais();

            await CriarUsuarioAsync(email, senha);
            var token = await ObterTokenAsync(email, senha);

            var client = Factory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        public async Task ExecuteInTransactionAsync(Func<IServiceProvider, Task> action)
        {
            using var scope = Factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BackDefaultContext>();

            await using var transaction = await context.Database.BeginTransactionAsync();

            await action(scope.ServiceProvider);

            await transaction.RollbackAsync();
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}