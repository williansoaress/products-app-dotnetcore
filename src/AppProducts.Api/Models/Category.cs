using System.ComponentModel.DataAnnotations;

namespace AppProducts.Api.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [MinLength(3, ErrorMessage ="Este campo deve ter entre 3 e 60 caracteres")]
        [MaxLength(60, ErrorMessage ="Este campo deve ter entre 3 e 60 caracteres")]
        public string Title { get; set; }
    }
}
