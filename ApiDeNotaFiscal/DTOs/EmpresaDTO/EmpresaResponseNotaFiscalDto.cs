using ApiDeNotaFiscal.DTOs.NotaFiscalDTO;
using ApiDeNotaFiscal.Models;

namespace ApiDeNotaFiscal.DTOs.EmpresaDTO
{
    public class EmpresaResponseNotaFiscalDto
    {
        public int EmpresaId { get; private set; }
        public string? NomeDaEmpresa { get; private set; }
        public string? RazaoSocial { get; private set; }
        public string? CnpjEmissor { get; private set; }
        public string? InscricaoEstadual { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public ICollection<NotaFiscalResponseDTO>? NotasFiscais { get; set; }
    }
}
