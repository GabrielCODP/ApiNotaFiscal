using System.ComponentModel.DataAnnotations;

namespace ApiDeNotaFiscal.DTOs.EmpresaDTO
{
    public class EmpresaResponseDTO
    {
        public int EmpresaId { get; private set; }
        public string? NomeDaEmpresa { get; private set; }
        public string? RazaoSocial { get; private set; }
        public string? CnpjEmissor { get; private set; }
        public string? InscricaoEstadual { get; private set; }
        public DateTime DataCadastro { get; private set; }
    }
}
