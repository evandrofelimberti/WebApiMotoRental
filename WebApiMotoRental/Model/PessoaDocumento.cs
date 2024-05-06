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

        public DateTime DataEmissao { get; set; }

        public byte ImagemDocumento { get; set; }

        [Required]
        public eTipoPessoaDocumento Tipo { get; set; } = eTipoPessoaDocumento.tpdNenhum;

        [ForeignKey("PessoaId")]
        [JsonIgnore]
        public Pessoa Pessoa { get; set; } = null!;
        public int PessoaId { get; set; }

        [JsonIgnore]
        public PessoaDocumentoCNH PessoaDocumentoCNH { get; set; } = null!;


    }

    public class PessoaDocumentoCNH
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey("PessoaDocumentoId")]
        [JsonIgnore]
        public PessoaDocumento PessoaDocumento { get; set; } = null!;
        public int PessoaDocumentoId { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime PrimeiraHabilitacao { get; set; }

        [Required]
        [JsonIgnore]
        public ICollection<PessoaDocumentoTipoCNH> PessoaDocumentoTipoCNH { get; set; } = new List<PessoaDocumentoTipoCNH>();
    }

    public class PessoaDocumentoTipoCNH
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey("PessoaDocumentoCNHId")]
        [JsonIgnore]
        public PessoaDocumentoCNH PessoaDocumentoCNH { get; set; } = null!;
        public int PessoaDocumentoCNHId { get; set; }

        public eTipoCNH TipoCNH { get; set; } = eTipoCNH.tcnhNenhum;
    }
}
