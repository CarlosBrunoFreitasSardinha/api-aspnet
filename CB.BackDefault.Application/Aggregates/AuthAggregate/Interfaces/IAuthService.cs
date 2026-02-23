using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces
{
    public interface IAuthService
    {
        // Realiza o login e retorna o token JWT
        Task<string?> LoginAsync(LoginViewModel model);

        // Cria um novo usuário no Identity
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);

        // Lida com o login via Google/Facebook
        // Task<string?> ExternalLoginAsync(ExternalLoginInfo info);

        // Método auxiliar que já tínhamos discutido
        Task<string> GerarJwt(string email);
    }
}
