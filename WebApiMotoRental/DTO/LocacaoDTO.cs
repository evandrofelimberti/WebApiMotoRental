using System.Text.Json.Serialization;

namespace WebApiMotoRental.DTO
{
    public class LocacaoDTO
    {
        //public int Id { get; set; }
        public string Descricao { get; set; } = String.Empty;
        public DateTime DataInclusao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        public DateTime DataTermino { get; set; }

        public int PessoaId { get; set; }

        public int PlanoLocacaoId { get; set; }

        public Double ValorTotalAluguel { get; set; } = 0.0;
        public int QuantidadeDiasAluguel { get; set; }

    }

    public class PlanoLocacaoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int QuantidadeDias { get; set; }
        public Double ValorDia { get; set; } = 0.0;
        public Double PercentualMulta { get; set; } = 0.0;
    }
}
