using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels;
using CB.BackDefault.IntegrationTests.Base;
using CB.BackDefault.IntegrationTests.Collections;
using System.Net.Http.Json;

namespace CB.BackDefault.IntegrationTests.Controllers
{
    [Collection(nameof(IntegrationApiCollection))]
    public class AuthApiTests
    {
        private readonly IntegrationTestsFixture<Program> _fixture;

        public AuthApiTests(IntegrationTestsFixture<Program> fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Deve registrar Usuario com Sucesso")]
        [Trait("Categoria", "Autorization controller")]
        public async Task Deve_Registrar_Com_Sucesso()
        {
            await _fixture.ExecuteInTransactionAsync(async sp =>
            {
                var client = _fixture.Factory.CreateClient();

                var email = $"teste_{Guid.NewGuid()}@sistema.com";
                var senha = "SenhaForte@123";

                var registerModel = new RegisterViewModel(email, senha, senha);

                var response = await client.PostAsJsonAsync("api/auth/register", registerModel);

                Assert.True(response.IsSuccessStatusCode);
            });
        }

        [Fact(DisplayName = "Deve Fazer Login com Sucesso")]
        [Trait("Categoria", "Autorization controller")]
        public async Task Deve_Fazer_Login_Com_Sucesso()
        {
            await _fixture.ExecuteInTransactionAsync(async sp =>
            {
                var client = _fixture.Factory.CreateClient();

                var email = $"teste_{Guid.NewGuid()}@sistema.com";
                var senha = "SenhaForte@123";

                // registra primeiro
                await client.PostAsJsonAsync("api/auth/register",
                    new RegisterViewModel(email, senha, senha));

                var loginResponse = await client.PostAsJsonAsync("api/auth/login",
                    new LoginViewModel(email, senha));

                var result = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();

                Assert.True(loginResponse.IsSuccessStatusCode);
                Assert.NotNull(result?.Token);
                Assert.NotEmpty(result.Token);
            });
        }

        private class LoginResult
        {
            public string Token { get; set; } = string.Empty;
        }
    }
}