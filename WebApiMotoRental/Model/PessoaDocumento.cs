using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApiMotoRental.Enum;

namespace WebApiMotoRental.Model
{
    [Index(nameof(Numero), nameof(Tipo), IsUnique = true)]
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

        [ForeignKey("PessoaId")]
        public Pessoa Pessoa { get; set; }
        public int PessoaId { get; set; }

        public PessoaDocumentoCNH PessoaDocumentoCNH { get; set; }


    }

    public class PessoaDocumentoCNH
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("PessoaDocumentoId")]
        public PessoaDocumento PessoaDocumento { get; set; }
        public int PessoaDocumentoId { get; set; }

        [Required]
        public ICollection<PessoaDocumentoTipoCNH> PessoaDocumentoTipoCNH { get; set; } = new List<PessoaDocumentoTipoCNH>();
    }

    public class PessoaDocumentoTipoCNH
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("PessoaDocumentoCNHId")]
        public PessoaDocumentoCNH PessoaDocumentoCNH { get; set; }
        public int PessoaDocumentoCNHId { get; set; }

        public eTipoCNH TipoCNH { get; set; } = eTipoCNH.tcnhNenhum;
    }
}
