using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDeNotaFiscal.DTOs.NotaFiscalDTO
{
    public class NotaFiscalCreateDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "O número da NF deve ter no máximo {1} caracteres")]
        public string? NumeroNF { get; set; }
        [Required]
        public decimal ValorTotal { get; set; }
        [JsonIgnore]
        public DateTime DataNF { get; private set; } = DateTime.Now;
        [Required]
        public int EmpresaId { get; set; }
        [Required]
        public int ClienteId { get; set; }

    }
}
