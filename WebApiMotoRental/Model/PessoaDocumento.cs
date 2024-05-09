using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApiMotoRental.DTO;
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
        public int PessoaId { get; set; }

        [JsonIgnore]
        public Pessoa Pessoa { get; set; } = null!;

        public PessoaDocumentoCNH PessoaDocumentoCNH { get; set; } = null!;

        public void FromPessoaDocumentoDTO(PessoaDocumentoDTO pessoaDocumentoDTO)
        {
            this.Numero = pessoaDocumentoDTO.Numero;
            this.DataEmissao = pessoaDocumentoDTO.DataEmissao;
            this.Tipo = pessoaDocumentoDTO.Tipo;
            this.PessoaId = pessoaDocumentoDTO.PessoaId;
            //this.ImagemDocumento = pessoaDocumentoDTO.ImagemDocumento;
            if (pessoaDocumentoDTO.PessoaDocumentoCNH != null)
            {
                PessoaDocumentoCNH pessoaDocumentoCNH = new PessoaDocumentoCNH();
                pessoaDocumentoCNH.FromPessoaDocumentoCNH(pessoaDocumentoDTO.PessoaDocumentoCNH);                 
                this.PessoaDocumentoCNH = pessoaDocumentoCNH;
            }

        }

    }

    public class PessoaDocumentoCNH
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey("PessoaDocumentoId")]
        public int PessoaDocumentoId { get; set; }

        //[JsonIgnore]
        public PessoaDocumento PessoaDocumento { get; set; } = null!;

        public DateTime DataVencimento { get; set; }

        public DateTime PrimeiraHabilitacao { get; set; }

       // [Required]
       // [JsonIgnore]
        public ICollection<PessoaDocumentoTipoCNH> PessoaDocumentoTipoCNH { get; set; } = new List<PessoaDocumentoTipoCNH>();

        public void FromPessoaDocumentoCNH(PessoaDocumentoCNHDTO pessoaDocumentoCNHDTO)
        {
            this.DataVencimento = pessoaDocumentoCNHDTO.DataVencimento;
            this.PrimeiraHabilitacao = pessoaDocumentoCNHDTO.PrimeiraHabilitacao;
            this.PessoaDocumentoId = pessoaDocumentoCNHDTO.PessoaDocumentoId;
            foreach (var item in pessoaDocumentoCNHDTO.PessoaDocumentoTipoCNH)
            {
                PessoaDocumentoTipoCNH pessoaDocumentoTipoCNH = new PessoaDocumentoTipoCNH();
                pessoaDocumentoTipoCNH.FromPessoaDocumentoTipoCNH(item);
                this.PessoaDocumentoTipoCNH.Add(pessoaDocumentoTipoCNH);
            }
        }
    }

    public class PessoaDocumentoTipoCNH
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey("PessoaDocumentoCNHId")]
        public int PessoaDocumentoCNHId { get; set; }

        //[JsonIgnore]
        public PessoaDocumentoCNH PessoaDocumentoCNH { get; set; } = new PessoaDocumentoCNH();

        public eTipoCNH TipoCNH { get; set; } = eTipoCNH.tcnhNenhum;

        public void FromPessoaDocumentoTipoCNH(PessoaDocumentoTipoCNHDTO pessoaDocumentoTipoCNHDTO)
        {
            this.PessoaDocumentoCNHId = pessoaDocumentoTipoCNHDTO.PessoaDocumentoCNHId;
            this.TipoCNH = pessoaDocumentoTipoCNHDTO.TipoCNH;
        }
    }
}
