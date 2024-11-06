using WebApiMotoRental.DTO;
using WebApiMotoRental.Enum;

namespace WebApiMotoRental.Interfaces
{
    public interface LocacaoServiceImpl
    {
        ValidacaoLocacaoResultado ValidarLocacao(LocacaoDTO locacaoDTO);

        void CadastrarLocacao(LocacaoDTO locacaoDTO);
        void DevolverVeiculo(int id, LocacaoDTO locacaoDTO);
    }
}
