using System.ComponentModel.DataAnnotations;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }
        public RegisterViewModel() { }

        public RegisterViewModel(string email, string senha, string confirmeSenha)
        {
            this.Email = email;
            this.Password = senha;
            this.ConfirmPassword = confirmeSenha;
        }
    }
}
