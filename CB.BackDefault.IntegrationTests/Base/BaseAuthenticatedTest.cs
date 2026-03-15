using CB.BackDefault.IntegrationTests.Collections;
using System.Net.Http.Headers;

namespace CB.BackDefault.IntegrationTests.Base;

[Collection(nameof(IntegrationApiCollection))]
public abstract class BaseAuthenticatedTest<TProgram> where TProgram : class
{
    protected readonly IntegrationTestsFixture<TProgram> Fixture;

    protected BaseAuthenticatedTest(IntegrationTestsFixture<TProgram> fixture)
    {
        Fixture = fixture;
    }

    protected async Task<HttpClient> CreateAuthenticatedClientAsync()
    {
        var (email, senha) = Fixture.GerarUsuarioCredenciais();

        await Fixture.CriarUsuarioAsync(email, senha);

        var token = await Fixture.ObterTokenAsync(email, senha);

        var client = Fixture.Factory.CreateClient();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return client;
    }
}