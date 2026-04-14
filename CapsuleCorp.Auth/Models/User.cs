using System.ComponentModel.DataAnnotations;

namespace CapsuleCorp.Auth.Models
{
    public class User
    {
        [Key] // Define explicitamente como chave primária
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(100)] // Evita que alguém envie um texto gigantesco e quebre o banco
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdateDate { get; set; }
    }
}