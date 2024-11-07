using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApiMotoRental.DTO;
using WebApiMotoRental.Enum;
using WebApiMotoRental.Factory;
using WebApiMotoRental.Model;
using Xunit;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using WebApiMotoRental.Data;

namespace MotoRentalTest
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });
            });
        }
    }    
    public class UnitTestLocacao: IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public UnitTestLocacao(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

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

        [Fact]
        public async Task CriarVeiculoPostTest()
        {
            //Arrange
            var veiculo = new VeiculoDTO()
            {
                Ano = "2020",
                Id = 1,
                Modelo = "Gol",
                Placa = "MKO9865"
            };

            // Act    
            var response = await _client.PostAsJsonAsync("api/Veiculo", veiculo);
            
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }

}