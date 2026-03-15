using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CB.BackDefault.IntegrationTests.Helpers;

public static class AuthHelper
{
    public static async Task<AuthResult> RegisterAndLoginAsync(HttpClient client)
    {
        var email = $"teste_{Guid.NewGuid()}@sistema.com";
        var senha = "SenhaForte@123";

        await client.PostAsJsonAsync("api/auth/register", new
        {
            Email = email,
            Password = senha,
            ConfirmPassword = senha
        });

        var response = await client.PostAsJsonAsync("api/auth/login", new
        {
            Email = email,
            Password = senha
        });

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<AuthResult>();

        return result!;
    }

    public static void AddToken(HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    public class AuthResult
    {
        public string Token { get; set; } = "";
        public string RefreshToken { get; set; } = "";
    }
}