using Microsoft.EntityFrameworkCore;
using WebApiMotoRental.Data;
using WebApiMotoRental.DTO;
using WebApiMotoRental.Enum;
using WebApiMotoRental.Factory;
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
                Locacao locacao = new Locacao();
                locacao.FromLocacaoDTO(locacaoDTO);
                locacao.PlanoLocacao = _dataContext.PlanoLocacao.Where(p => p.Id == locacaoDTO.PlanoLocacaoId).FirstOrDefault();

                _dataContext.Locacao.Add(locacao);
                _dataContext.SaveChanges();
            }
        }

        public void DevolverVeiculo(int id, LocacaoDTO locacaoDTO)
        {
            var resultValidacao = ValidarLocacao(locacaoDTO);
            if ((resultValidacao & ValidacaoLocacaoResultado.Ok) == ValidacaoLocacaoResultado.Ok)
            {
                var locacao = _dataContext.Locacao.Find(id);
                if (locacao == null) throw new ArgumentNullException("Locação inválida!!!");

                locacao.FromLocacaoDTO(locacaoDTO);
                locacao.PlanoLocacao = _dataContext.PlanoLocacao.Where(p => p.Id == locacaoDTO.PlanoLocacaoId).FirstOrDefault();

                var locacaoCalculoTipo = BuscarLocacaoCalculoTipo(locacaoDTO);

                LocacaoCalculoFactory locacaoCalculoFactory = new LocacaoCalculoFactory();
                var locacaoCalculo = locacaoCalculoFactory.CreateLocacaoCalculo(locacaoCalculoTipo);
                locacao.ValorTotalAluguel = locacaoCalculo.CalcularValor(locacao);

                _dataContext.Entry(locacao).State = EntityState.Modified;
                _dataContext.SaveChanges();
            }

        }

        private LocacaoCalculoTipo BuscarLocacaoCalculoTipo(LocacaoDTO locacaoDTO)
        {
            if (locacaoDTO.DataTermino == locacaoDTO.DataPrevisaoTermino)
            {
                return LocacaoCalculoTipo.PeriodoExatoTermino;
            } else
            if (locacaoDTO.DataTermino < locacaoDTO.DataPrevisaoTermino)
            {
                return LocacaoCalculoTipo.PeriodoInferiorTermino;
            } else
            if (locacaoDTO.DataTermino > locacaoDTO.DataPrevisaoTermino)
            {
                return LocacaoCalculoTipo.PeriodoSuperiorTermino;
            } else
                return LocacaoCalculoTipo.Default;
        }

        private void DefinirPlanoLocacao(LocacaoDTO locacaoDTO)
        {
            var plano = _dataContext.PlanoLocacao.Where(p => p.QuantidadeDias == locacaoDTO.QuantidadeDiasAluguel).FirstOrDefault();
            if (plano == null)
            {
                throw new Exception("Plano locação não encontrado! \nVerifique a quantidade de dias informado para locação!");
            }

            locacaoDTO.PlanoLocacaoId = plano.Id;
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
            if (!LocacaoPossuiQuantidadeDiasComPlanoLocacao(locacaoDTO.QuantidadeDiasAluguel))
            {
                result |= ValidacaoLocacaoResultado.NumeroDiasForadoPlanoDisponivel;
            }

                if (!result.HasFlag(ValidacaoLocacaoResultado.InicioLocacaoPrimeiroDiaAposInclusao) ||
                !result.HasFlag(ValidacaoLocacaoResultado.DataInicioNaoInformada) ||
                !result.HasFlag(ValidacaoLocacaoResultado.DataTerminoNaoInformada) ||
                !result.HasFlag(ValidacaoLocacaoResultado.DataPrevisaoTerminoNaoInformada)||
                !result.HasFlag(ValidacaoLocacaoResultado.EntregadorNaohabilitadoCategoriaA)||
                !result.HasFlag(ValidacaoLocacaoResultado.NumeroDiasForadoPlanoDisponivel))
            {
                result |= ValidacaoLocacaoResultado.Ok;
            }

            return result;
        }
        private bool LocacaoPossuiQuantidadeDiasComPlanoLocacao(int quantidadeDiasAluguel)
        {
            var possuiPlano = _dataContext.PlanoLocacao.Where(p => p.QuantidadeDias == quantidadeDiasAluguel);
            return possuiPlano.Any();
        }
    }
}
