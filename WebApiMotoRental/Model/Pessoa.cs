using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApiMotoRental.DTO;

namespace WebApiMotoRental.Model
{
    public class Pessoa
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        //[JsonIgnore]
        public ICollection<PessoaDocumento> PessoaDocumento { get; set; }  = new List<PessoaDocumento>();
        
        public Pessoa()
        {

        }

        public void FromPessoaDTO(PessoaDTO pessoaDTO)
        {
            this.Nome = pessoaDTO.Nome;
            this.DataNascimento = pessoaDTO.DataNascimento;
            this.IncluirDocumento(pessoaDTO);
                
        }

        public void IncluirDocumento(PessoaDTO pessoaDTO)
        {
            foreach (var item in pessoaDTO.PessoaDocumento)
            {
                item.PessoaId = this.Id;
                PessoaDocumento pessoaDocumento = new PessoaDocumento();
                pessoaDocumento.FromPessoaDocumentoDTO(item);

                this.PessoaDocumento.Add(pessoaDocumento);
            } 
            
        }

    }

   
}
