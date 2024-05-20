using WebApiMotoRental.DTO;
using WebApiMotoRental.Enum;
using WebApiMotoRental.Factory;
using WebApiMotoRental.Model;

namespace MotoRentalTest
{
    public class UnitTestLocacao
    {
        [Fact]
        public void TestLocacaoCalculoPeriodoExato()
        {

            LocacaoDTO locacaoDTO = new LocacaoDTO();
            locacaoDTO.DataInclusao = DateTime.Now;
            locacaoDTO.DataInicio = locacaoDTO.DataInclusao.AddDays(1);
            locacaoDTO.DataPrevisaoTermino = locacaoDTO.DataInclusao.AddDays(7);
            locacaoDTO.DataTermino = locacaoDTO.DataInclusao.AddDays(7);
            locacaoDTO.Descricao = "Teste locacao";
            locacaoDTO.PessoaId = 0;
            locacaoDTO.PlanoLocacaoId = 0;
            locacaoDTO.QuantidadeDiasAluguel = 7;

            Locacao locacao = new Locacao();
            locacao.FromLocacaoDTO(locacaoDTO);
            locacao.PlanoLocacao = new PlanoLocacao {Descricao = "Teste", QuantidadeDias = 7, PercentualMulta = 20, ValorDia = 50};
            Double valorAluguelDiasExato = 350;            

            var locacaoCalculoTipo = LocacaoCalculoTipo.PeriodoExatoTermino;
            LocacaoCalculoFactory locacaoCalculoFactory = new LocacaoCalculoFactory();
            var locacaoCalculo = locacaoCalculoFactory.CreateLocacaoCalculo(locacaoCalculoTipo);
            var valorAlugel = locacaoCalculo.CalcularValor(locacao);

            Assert.Equal(valorAlugel, valorAluguelDiasExato);

        }
        [Fact]
        public void TestLocacaoCalculoPeriodoInferior()
        {
            LocacaoDTO locacaoDTO = new LocacaoDTO();
            locacaoDTO.DataInclusao = DateTime.Now;
            locacaoDTO.DataInicio = locacaoDTO.DataInclusao.AddDays(1);
            locacaoDTO.DataPrevisaoTermino = locacaoDTO.DataInclusao.AddDays(7);
            locacaoDTO.DataTermino = locacaoDTO.DataInclusao.AddDays(5);
            locacaoDTO.Descricao = "Teste locacao";
            locacaoDTO.PessoaId = 0;
            locacaoDTO.PlanoLocacaoId = 0;
            locacaoDTO.QuantidadeDiasAluguel = 5;

            Locacao locacao = new Locacao();
            locacao.FromLocacaoDTO(locacaoDTO);
            locacao.PlanoLocacao = new PlanoLocacao { Descricao = "Teste", QuantidadeDias = 7, PercentualMulta = 20, ValorDia = 50 };
            Double valorAluguelDiasExato = 370;

            var locacaoCalculoTipo = LocacaoCalculoTipo.PeriodoInferiorTermino;
            LocacaoCalculoFactory locacaoCalculoFactory = new LocacaoCalculoFactory();
            var locacaoCalculo = locacaoCalculoFactory.CreateLocacaoCalculo(locacaoCalculoTipo);
            var valorAlugel = locacaoCalculo.CalcularValor(locacao);

            Assert.Equal(valorAlugel, valorAluguelDiasExato);
        }

        [Fact]
        public void TestLocacaoCalculoPeriodoSuperior()
        {
            LocacaoDTO locacaoDTO = new LocacaoDTO();
            locacaoDTO.DataInclusao = DateTime.Now;
            locacaoDTO.DataInicio = locacaoDTO.DataInclusao.AddDays(1);
            locacaoDTO.DataPrevisaoTermino = locacaoDTO.DataInclusao.AddDays(7);
            locacaoDTO.DataTermino = locacaoDTO.DataInclusao.AddDays(9);
            locacaoDTO.Descricao = "Teste locacao";
            locacaoDTO.PessoaId = 0;
            locacaoDTO.PlanoLocacaoId = 0;
            locacaoDTO.QuantidadeDiasAluguel = 9;

            Locacao locacao = new Locacao();
            locacao.FromLocacaoDTO(locacaoDTO);
            locacao.PlanoLocacao = new PlanoLocacao { Descricao = "Teste", QuantidadeDias = 7, PercentualMulta = 20, ValorDia = 50 };
            Double valorAluguelDiasExato = 550;

            var locacaoCalculoTipo = LocacaoCalculoTipo.PeriodoSuperiorTermino;

            LocacaoCalculoFactory locacaoCalculoFactory = new LocacaoCalculoFactory();
            var locacaoCalculo = locacaoCalculoFactory.CreateLocacaoCalculo(locacaoCalculoTipo);
            var valorAlugel = locacaoCalculo.CalcularValor(locacao);

            Assert.Equal(valorAlugel, valorAluguelDiasExato);
        }
    }
}