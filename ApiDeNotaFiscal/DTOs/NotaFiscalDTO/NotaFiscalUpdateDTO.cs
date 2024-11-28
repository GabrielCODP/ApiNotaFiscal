using System.ComponentModel.DataAnnotations;

namespace ApiDeNotaFiscal.DTOs.NotaFiscalDTO
{
    public class NotaFiscalUpdateDTO : IValidatableObject
    {

        [Key]
        public int NotaFiscalId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O número da NF deve ter no máximo {1} caracteres")]
        public string? NumeroNF { get; set; }
        [Required]
        public decimal ValorTotal { get; set; }
        public DateTime DataNF { get; set; }
        [Required]
        public int EmpresaId { get; set; }
        [Required]
        public int ClienteId { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataNF.Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("A data deve ser maior que a data atual",
                  new[] { nameof(this.DataNF) });
            }
        }
    }


}
