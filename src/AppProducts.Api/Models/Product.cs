using System.ComponentModel.DataAnnotations;

namespace AppProducts.Api.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(1024, ErrorMessage ="Este campo deve possuir no máximo {0} caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [Range(0.1, int.MaxValue, ErrorMessage = "O preço deve ser maior que {0}")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
