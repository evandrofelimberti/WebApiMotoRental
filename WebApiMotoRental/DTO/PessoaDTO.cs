using WebApiMotoRental.Model;

namespace WebApiMotoRental.DTO
{
    public class PessoaDTO
    {
        public string Nome { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public ICollection<PessoaDocumentoDTO> PessoaDocumento { get; set; } = new List<PessoaDocumentoDTO>();
    }
}
