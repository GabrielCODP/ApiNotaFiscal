using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDeNotaFiscal.DTOs.EmpresaDTO
{
    public class EmpresaCreateDTO
    {
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


        [JsonIgnore]
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
    }
}
