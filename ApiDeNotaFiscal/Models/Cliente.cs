using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDeNotaFiscal.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        [StringLength(80)]
        public string? NomeDoCliente { get; set; }

        [Required(ErrorMessage = "O endereço do cliente é obrigatório")]
        [StringLength(80)]
        public string? EnderecoDoCliente { get; set; }

        [Required]
        [MinLength(14, ErrorMessage = "O CNPJ deve ter no mínimo {1} caracteres")]
        [StringLength(14, ErrorMessage = "O CNPJ deve ter {1} caracteres")]
        public string? CNPJCliente { get; set; }

        public DateTime DataCadastro { get; set; }

        public ICollection<NotaFiscal> NotaFiscals { get; set; }

        public Cliente()
        {
            NotaFiscals = new Collection<NotaFiscal>();
        }


    }
}
