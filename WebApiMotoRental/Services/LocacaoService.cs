using Microsoft.EntityFrameworkCore;
using WebApiMotoRental.Data;
using WebApiMotoRental.DTO;
using WebApiMotoRental.Enum;
using WebApiMotoRental.Interfaces;
using WebApiMotoRental.Model;

namespace WebApiMotoRental.Services
{
    public class LocacaoService : LocacaoServiceImpl
    {
        protected DataContext _dataContext;

        public LocacaoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void CadastrarLocacao(LocacaoDTO locacaoDTO)
        {
            var resultValidacao = ValidarLocacao(locacaoDTO);
            if ((resultValidacao & ValidacaoLocacaoResultado.Ok) == ValidacaoLocacaoResultado.Ok)
            {
                LocacaoCalculoFactory locacaoCalculoFactory = new LocacaoCalculoFactory();
                var locacaoCalculo = locacaoCalculoFactory.CreateLocacaoCalculo(LocacaoCalculoTipo.Default);
                locacaoDTO.ValorTotalAluguel = locacaoCalculo.CalcularValor();
                locacaoDTO.QuantidadeDiasAluguel = locacaoCalculo.CalcularDiasLocado();

                Locacao locacao = new Locacao();
                locacao.FromLocacaoDTO(locacaoDTO);

                _dataContext.Locacao.Add(locacao);
                _dataContext.SaveChanges();
            }
        }

        private bool PessoaPossuiDocumentoCategoriaA(int pessoaId)
        {
            var possuiDocumentoTipoA = _dataContext.Pessoa
                .Where(p => p.Id == pessoaId)
                .Include(d => d.PessoaDocumento)
                .ThenInclude(c => c.PessoaDocumentoCNH)
                .ThenInclude(t => t.PessoaDocumentoTipoCNH.Where(ta => ta.TipoCNH == eTipoCNH.tcnhA));

            return (possuiDocumentoTipoA.Count() > 0);
        }

        public ValidacaoLocacaoResultado ValidarLocacao(LocacaoDTO locacaoDTO)
        {
            if (locacaoDTO == null)
            {
                throw new ArgumentNullException("locacaoDTO");
            }

            var result = ValidacaoLocacaoResultado.Default;

            if (locacaoDTO.DataInicio == DateTime.MinValue)
            {
                result |= ValidacaoLocacaoResultado.DataInicioNaoInformada;
            }

            if (locacaoDTO.DataTermino == DateTime.MinValue)
            {
                result |= ValidacaoLocacaoResultado.DataTerminoNaoInformada;
            }

            if (locacaoDTO.DataPrevisaoTermino == DateTime.MinValue)
            {
                result |= ValidacaoLocacaoResultado.DataPrevisaoTerminoNaoInformada;
            }

            if (!(locacaoDTO.DataInclusao.AddDays(1) == locacaoDTO.DataInicio))
            {
                result |= ValidacaoLocacaoResultado.InicioLocacaoPrimeiroDiaAposInclusao;
            }
            if (!PessoaPossuiDocumentoCategoriaA(locacaoDTO.PessoaId))
            {
                result |= ValidacaoLocacaoResultado.EntregadorNaohabilitadoCategoriaA;
            }

            if (!result.HasFlag(ValidacaoLocacaoResultado.InicioLocacaoPrimeiroDiaAposInclusao) ||
                !result.HasFlag(ValidacaoLocacaoResultado.DataInicioNaoInformada) ||
                !result.HasFlag(ValidacaoLocacaoResultado.DataTerminoNaoInformada) ||
                !result.HasFlag(ValidacaoLocacaoResultado.DataPrevisaoTerminoNaoInformada)||
                !result.HasFlag(ValidacaoLocacaoResultado.EntregadorNaohabilitadoCategoriaA))
            {
                result |= ValidacaoLocacaoResultado.Ok;
            }

            return result;
        }
    }
}
