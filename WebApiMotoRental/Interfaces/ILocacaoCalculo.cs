using WebApiMotoRental.DTO;
using WebApiMotoRental.Enum;
using WebApiMotoRental.Model;

namespace WebApiMotoRental.Interfaces
{
    public interface LocacaoCalculoImpl
    {
        double CalcularValor(Locacao locacao);
    }    
}
