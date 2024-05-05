using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMotoRental.Model
{
    public class Pessoa
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public ICollection<PessoaDocumento> PessoaDocumentos { get; set; }  = new List<PessoaDocumento>();
    }

   
}
