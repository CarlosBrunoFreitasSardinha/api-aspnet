using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels;
using CB.BackDefault.IntegrationTests.Base;
using System.Net.Http.Json;
using Xunit;

namespace CB.BackDefault.IntegrationTests.Aggregates.AuthAggregate
{
    public class AuthApiTests : IClassFixture<IntegrationTestBase>
    {
        private readonly HttpClient _client;

        public AuthApiTests(IntegrationTestBase factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Deve_Registrar_E_Fazer_Login_Com_Sucesso()
        {
            // Arrange - Usamos GUID para nunca repetir email entre execuções
            var email = $"teste_{Guid.NewGuid()}@sistema.com";
            var senha = "SenhaForte@123";

            var registerModel = new RegisterViewModel
            {
                Email = email,
                Password = senha,
                ConfirmPassword = senha
            };

            var loginModel = new LoginViewModel
            {
                Email = email,
                Password = senha
            };

            // Act - 1. Registrar
            var registerResponse = await _client.PostAsJsonAsync("api/auth/register", registerModel);
            var registerContent = await registerResponse.Content.ReadAsStringAsync();

            Assert.True(registerResponse.IsSuccessStatusCode,
                $"Falha no Registro. Status: {registerResponse.StatusCode}. Erro: {registerContent}");

            // Act - 2. Login
            var loginResponse = await _client.PostAsJsonAsync("api/auth/login", loginModel);
            var loginContent = await loginResponse.Content.ReadAsStringAsync();

            Assert.True(loginResponse.IsSuccessStatusCode,
                $"Falha no Login. Status: {loginResponse.StatusCode}. Erro: {loginContent}");

            var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();

            // Assert
            Assert.NotNull(loginResult?.Token);
            Assert.NotEmpty(loginResult.Token);
        }

        private class LoginResult { public string Token { get; set; } = string.Empty; }
    }
}