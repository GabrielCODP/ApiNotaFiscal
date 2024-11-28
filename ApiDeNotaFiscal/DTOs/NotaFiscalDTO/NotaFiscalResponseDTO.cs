using ApiDeNotaFiscal.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiDeNotaFiscal.DTOs.NotaFiscalDTO
{
    public class NotaFiscalResponseDTO
    {
        public int NotaFiscalId { get; set; }
        public string? NumeroNF { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataNF { get; set; }
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
    }
}
