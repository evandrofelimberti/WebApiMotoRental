using WebApiMotoRental.DTO;
using WebApiMotoRental.Enum;
using WebApiMotoRental.Interfaces;
using WebApiMotoRental.Model;

namespace WebApiMotoRental.Factory
{
    public class LocacaoCalculoFactory
    {
        public LocacaoCalculoImpl CreateLocacaoCalculo(LocacaoCalculoTipo tipo)
        {
            switch (tipo)
            {
                case LocacaoCalculoTipo.PeriodoExatoTermino:
                    return new LocacalCalculoPeriodoExatoTermino();
                case LocacaoCalculoTipo.PeriodoInferiorTermino:
                    return new LocacaoCalculoPeriodoInferiorTermino();
                case LocacaoCalculoTipo.PeriodoSuperiorTermino:
                    return new LocacaoCalculoPeriodoSuperiorTermino();
                default: throw new ArgumentException("Tipo locacao calculo inválido!");

            }
        }
    }

    public class LocacalCalculoPeriodoExatoTermino : LocacaoCalculoImpl
    {
        public double CalcularValor(Locacao locacao)
        {
            return (locacao.QuantidadeDiasAluguel * locacao.PlanoLocacao.ValorDia);
        }
    }

    public class LocacaoCalculoPeriodoInferiorTermino : LocacaoCalculoImpl
    {
        public double CalcularValor(Locacao locacao)
        {
            Double valorAluguel = (locacao.QuantidadeDiasAluguel * locacao.PlanoLocacao.ValorDia);
            int DiariaNaoEfetivada = (locacao.DataPrevisaoTermino - locacao.DataTermino).Days;
            Double valorAluguelNaoEfetivado = (DiariaNaoEfetivada * locacao.PlanoLocacao.ValorDia);
            valorAluguelNaoEfetivado += (valorAluguelNaoEfetivado * (locacao.PlanoLocacao.PercentualMulta / 100));
            valorAluguel += valorAluguelNaoEfetivado;
            return valorAluguel;
        }
    }

    public class LocacaoCalculoPeriodoSuperiorTermino : LocacaoCalculoImpl
    {
        public double CalcularValor(Locacao locacao)
        {
            const Double ValorDiaExcedido = 50.00;
            Double valorAluguel = (locacao.QuantidadeDiasAluguel * locacao.PlanoLocacao.ValorDia);
            int DiasExcedidos = (locacao.DataTermino - locacao.DataPrevisaoTermino).Days;
            valorAluguel += (DiasExcedidos * ValorDiaExcedido);
            return valorAluguel;
        }
    }
}
