using System.ComponentModel.DataAnnotations;

namespace ApiDeNotaFiscal.DTOs.ClienteDTOs
{
    public class ClienteResponseDTO
    {
        public int ClienteId { get; set; }
        public string? NomeDoCliente { get; set; }
        public string? EnderecoDoCliente { get; set; }
        public string? CNPJCliente { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
