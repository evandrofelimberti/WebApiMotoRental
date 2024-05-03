using System.ComponentModel.DataAnnotations;
using WebApiMotoRental.Enum;

namespace WebApiMotoRental.Model
{
    public class PessoaDocumento
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Numero { get; set; } = string.Empty;

        public byte ImagemDocumento { get; set; }

        [Required]
        public eTipoPessoaDocumento Tipo { get; set; } = eTipoPessoaDocumento.tpdNenhum;
    }

    public class PessoaDocumentoCNH: PessoaDocumento
    {
        [Required]
        public ICollection<eTipoCNH> eTipoCNHs { get; set; } = new List<eTipoCNH>();
    }
}
