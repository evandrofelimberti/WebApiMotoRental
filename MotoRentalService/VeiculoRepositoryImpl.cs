using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiMotoRental.DTO;

namespace MotoRentalService
{
    public interface VeiculoRepositoryImpl
    {
        void CadastrarVeiculo(VeiculoDTO veiculoDto);
    }
}
