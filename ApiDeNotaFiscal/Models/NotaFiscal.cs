using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiDeNotaFiscal.Models
{
    public class NotaFiscal
    {
        [Key]
        public int NotaFiscalId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O número da NF deve ter no máximo {1} caracteres")]
        public string? NumeroNF { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotal { get; set; }
        public DateTime DataNF { get; set; }

        public int EmpresaId { get; set; }

        [JsonIgnore]
        public Empresa? Empresa { get; set; }

        public int ClienteId { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }

    }
}
