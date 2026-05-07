using System.ComponentModel.DataAnnotations;

namespace CapsuleCorp.Auth.DTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; } = string.Empty;

        public string? CurrentPassword { get; set; }

        [MinLength(6, ErrorMessage = "A nova senha deve ter no mínimo 6 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "A senha deve conter ao menos uma letra minúscula, uma maiúscula, um número e um caractere especial.")]
        public string? NewPassword { get; set; }
    }
}