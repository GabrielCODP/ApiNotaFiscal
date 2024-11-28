using System.ComponentModel.DataAnnotations;

namespace ApiDeNotaFiscal.DTOs.ClienteDTOs
{
    public class ClienteUpdateRequestDTO : IValidatableObject
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        [StringLength(80)]
        public string? NomeDoCliente { get; set; }

        [Required(ErrorMessage = "O endereço do cliente é obrigatório")]
        [StringLength(80)]
        public string? EnderecoDoCliente { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter {1} caracteres")]
        public string? CNPJCliente { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataCadastro.Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("A data deve ser maior que a data atual",
                  new[] { nameof(this.DataCadastro) });
            }
        }
    }
}
