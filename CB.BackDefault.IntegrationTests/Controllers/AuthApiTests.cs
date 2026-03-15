using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels;
using CB.BackDefault.IntegrationTests.Base;
using CB.BackDefault.IntegrationTests.Collections;
using System.Net;
using System.Net.Http.Json;

namespace CB.BackDefault.IntegrationTests.Controllers;

public class AuthApiTests : BaseAuthenticatedTest<Program>
{
    public AuthApiTests(IntegrationTestsFixture<Program> fixture) : base(fixture)
    {
    }

    [Fact(DisplayName = "Deve registrar usuário com sucesso")]
    [Trait("Categoria", "Autorization controller")]
    public async Task Deve_Registrar_Com_Sucesso()
    {
        var client = Fixture.Factory.CreateClient();

        var (email, senha) = Fixture.GerarUsuarioCredenciais();

        var response = await client.PostAsJsonAsync("api/auth/register",
            new RegisterViewModel(email, senha, senha));

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "Deve fazer login com sucesso")]
    [Trait("Categoria", "Autorization controller")]
    public async Task Deve_Fazer_Login_Com_Sucesso()
    {
        var client = Fixture.Factory.CreateClient();

        var (email, senha) = Fixture.GerarUsuarioCredenciais();

        await Fixture.CriarUsuarioAsync(email, senha);

        var response = await client.PostAsJsonAsync("api/auth/login",
            new LoginViewModel(email, senha));

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "Deve retornar 401 sem token")]
    [Trait("Categoria", "Autorization controller")]
    public async Task Deve_Retornar_401_Sem_Token()
    {
        var client = Fixture.Factory.CreateClient();

        var response = await client.PostAsync("api/auth/logout", null);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact(DisplayName = "Deve fazer logout com sucesso")]
    [Trait("Categoria", "Autorization controller")]
    public async Task Deve_Fazer_Logout()
    {
        var client = await CreateAuthenticatedClientAsync();

        var response = await client.PostAsync("api/auth/logout", null);

        Assert.True(response.IsSuccessStatusCode);
    }
}