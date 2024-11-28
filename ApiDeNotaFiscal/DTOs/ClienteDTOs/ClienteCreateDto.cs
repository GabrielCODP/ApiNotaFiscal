using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDeNotaFiscal.DTOs.ClienteDTOs
{
    public class ClienteCreateDto
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        [StringLength(80)]
        public string? NomeDoCliente { get; set; }

        [Required(ErrorMessage = "O endereço do cliente é obrigatório")]
        [StringLength(80)]
        public string? EnderecoDoCliente { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter {1} caracteres")]
        public string? CNPJCliente { get; set; }

        [JsonIgnore]
        public DateTime DataCadastro { get;private set; } = DateTime.Now;
    }
}
