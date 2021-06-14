using System.ComponentModel.DataAnnotations;

namespace AppProducts.Api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MinLength(8, ErrorMessage = "Este campo deve ter entre {1} e 60 caracteres")]
        [MaxLength(60, ErrorMessage = "Este campo deve ter entre 8 e 60 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MinLength(8, ErrorMessage = "Este campo deve ter entre {1} e 60 caracteres")]
        [MaxLength(60, ErrorMessage = "Este campo deve ter entre 8 e 60 caracteres")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
