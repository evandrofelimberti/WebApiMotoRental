using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiMotoRental.DTO;

namespace WebApiMotoRental.Model
{
    public class Locacao
    {
        [Required]
        [Key]
        public int Id { get; set; } 
        public string Descricao { get; set; } = String.Empty;
        public DateTime DataInclusao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        public DateTime DataTermino { get; set; }

        [ForeignKey("PlanoLocacaoId")]
        public int PlanoLocacaoId { get; set; }

        public PlanoLocacao PlanoLocacao { get; set; } = null!;

        public Double ValorTotalAluguel { get; set; } = 0.0;
        public int QuantidadeDiasAluguel { get; set; }

        public void FromLocacaoDTO(LocacaoDTO locacaoDTO)
        {
            this.Id = locacaoDTO.Id;
            this.Descricao = locacaoDTO.Descricao;
            this.DataInclusao = locacaoDTO.DataInclusao;    
            this.DataInicio = locacaoDTO.DataInicio;
            this.DataPrevisaoTermino = locacaoDTO.DataPrevisaoTermino;
            this.DataTermino = locacaoDTO.DataTermino;
            this.ValorTotalAluguel = locacaoDTO.ValorTotalAluguel;
            this.PlanoLocacaoId = locacaoDTO.PlanoLocacaoId;
            this.QuantidadeDiasAluguel = locacaoDTO.QuantidadeDiasAluguel;         
        }
    }

    public class PlanoLocacao
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int QuantidadeDias { get; set; }
        public Double ValorDia { get; set; } = 0.0; 

    }

}
