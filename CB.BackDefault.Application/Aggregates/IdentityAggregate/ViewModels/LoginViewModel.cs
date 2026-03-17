using System.ComponentModel.DataAnnotations;

namespace CB.BackDefault.Application.Aggregates.IdentityAggregate.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; }

        public LoginViewModel() { }
        public LoginViewModel(string email, string senha) { 
            this.Email = email;
            this.Password = senha;
        }
    }
}
