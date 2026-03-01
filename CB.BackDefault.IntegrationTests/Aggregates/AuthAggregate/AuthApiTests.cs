using System;
using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels;
using CB.BackDefault.IntegrationTests.Base;
using System.Net.Http.Json;

namespace CB.BackDefault.IntegrationTests.Aggregates.AuthAggregate
{
    public class AuthApiTests : IClassFixture<IntegrationTestBase>
    {
        private readonly HttpClient _client;
        private readonly string _email;
        private readonly string _senha;

        public AuthApiTests(IntegrationTestBase factory)
        {
            _client = factory.CreateClient();
            _email = $"teste_{Guid.NewGuid()}@sistema.com";
            _senha = "SenhaForte@123";
        }

        [Fact(DisplayName = "Deve registrar Usuario com Sucesso")]
        [Trait("Agregate Identity", "Autorization controller")]
        public async Task Deve_Registrar_Com_Sucesso()
        {
            // Arrange
            var registerModel = new RegisterViewModel(_email, _senha, _senha);


            // Act - 1. Registrar
            var registerResponse = await _client.PostAsJsonAsync("api/auth/register", registerModel);
            var registerContent = await registerResponse.Content.ReadAsStringAsync();


            // Assert
            Assert.True(registerResponse.IsSuccessStatusCode,
                $"Falha no Registro. Status: {registerResponse.StatusCode}. Erro: {registerContent}");

        }

        [Fact(DisplayName = "Deve Fazer Login com Sucesso")]
        [Trait("Agregate Identity", "Autorization controller")]
        public async Task Deve_Fazer_Login_Com_Sucesso()
        {
            // Arrange
            var registerModel = new RegisterViewModel(_email, _senha, _senha);
            var loginModel = new LoginViewModel(_email, _senha);

            var registerResponse = await _client.PostAsJsonAsync("api/auth/register", registerModel);
            var registerContent = await registerResponse.Content.ReadAsStringAsync();


            // Act - 2. Login
            var loginResponse = await _client.PostAsJsonAsync("api/auth/login", loginModel);
            var loginContent = await loginResponse.Content.ReadAsStringAsync();


            // Assert
            Assert.True(loginResponse.IsSuccessStatusCode,
                $"Falha no Login. Status: {loginResponse.StatusCode}. Erro: {loginContent}");

            var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
            Assert.NotNull(loginResult?.Token);
            Assert.NotEmpty(loginResult.Token);
        }

        private class LoginResult { public string Token { get; set; } = string.Empty; }
    }
}