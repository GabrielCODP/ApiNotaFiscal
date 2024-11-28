using System.ComponentModel.DataAnnotations;

namespace ApiDeNotaFiscal.DTOs.EmpresaDTO
{
    public class EmpresaUpdateRequestDTO : IValidatableObject
    {
        [Key]
        public int EmpresaId { get; set; }

        [Required(ErrorMessage = "O nome da empresa é obrigatório")]
        [StringLength(80, ErrorMessage = "O nome da empresa deve ter no máximo {1} caracteres")]
        public string? NomeDaEmpresa { get; set; }

        [Required(ErrorMessage = "O nome da razão social é obrigatório")]
        [StringLength(80, ErrorMessage = "O nome da razão social deve ter no máximo {1} caracteres")]
        public string? RazaoSocial { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter {1} caracteres")]
        public string? CnpjEmissor { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "A inscricao estatual deve ter {1} caracteres")]
        public string? InscricaoEstadual { get; set; }

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
