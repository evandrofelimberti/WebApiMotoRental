using WebApiMotoRental.Enum;
using WebApiMotoRental.Model;

namespace WebApiMotoRental.DTO
{
    public class PessoaDocumentoDTO
    {

        public string Numero { get; set; } = string.Empty;

        public DateTime DataEmissao { get; set; }

        public byte ImagemDocumento { get; set; }

        public eTipoPessoaDocumento Tipo { get; set; } = eTipoPessoaDocumento.tpdNenhum;

        public int PessoaId { get; set; }

        public PessoaDocumentoCNHDTO PessoaDocumentoCNH { get; set; } = null!;    

    }

    public class PessoaDocumentoCNHDTO
    {
        public int PessoaDocumentoId { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime PrimeiraHabilitacao { get; set; }

        public ICollection<PessoaDocumentoTipoCNHDTO> PessoaDocumentoTipoCNH { get; set; } = new List<PessoaDocumentoTipoCNHDTO>();
    }

    public class PessoaDocumentoTipoCNHDTO
    {
        public int PessoaDocumentoCNHId { get; set; }

        public eTipoCNH TipoCNH { get; set; } = eTipoCNH.tcnhNenhum;
    }
}
