using CB.BackDefault.Domain.Aggregates.AuthAggregate.Models;
using Microsoft.AspNetCore.Identity;

namespace CB.BackDefault.Infra.Data.Shared.Identity
{
    public class UserAplication : IdentityUser
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string urlPerfil { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
        public ICollection<RefreshTokenModel> RefreshTokens { get; set; } = new List<RefreshTokenModel>();

        public UserAplication() { }
    }
}
