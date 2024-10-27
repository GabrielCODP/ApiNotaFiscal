using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDeNotaFiscal.Models
{
    public class Empresa
    {
        [Key]
        public int EmpresaId { get; set; }

        [Required(ErrorMessage = "O nome da empresa é obrigatório")]
        [StringLength(80)]
        public string? NomeDaEmpresa { get; set; }

        [Required(ErrorMessage = "O nome da razão social é obrigatório")]
        [StringLength(80)]
        public string? RazaoSocial { get; set; }

        [Required]
        [MinLength(14, ErrorMessage = "O CNPJ deve ter no mínimo {1} caracteres")]
        [StringLength(14, ErrorMessage = "O CNPJ deve ter {1} caracteres")]
        public string? CnpjEmissor { get; set; }

        [Required]
        [MinLength(9, ErrorMessage = "A inscricao estatual deve ter no mínimo {1} caracteres")]
        [StringLength(9, ErrorMessage = "A inscricao estatual deve ter {1} caracteres")]
        public string? InscricaoEstadual { get; set; }

        public DateTime DataCadastro { get; set; }

        public ICollection<NotaFiscal> NotasFiscais { get; set; }

        public Empresa()
        {
            NotasFiscais = new Collection<NotaFiscal>();
        }





    }
}
