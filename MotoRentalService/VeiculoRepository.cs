using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiMotoRental.Data;
using WebApiMotoRental.DTO;
using WebApiMotoRental.Model;

namespace MotoRentalService
{
    public class VeiculoRepository : VeiculoRepositoryImpl
    {
        private readonly DataContext _dataContext;

        public VeiculoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void CadastrarVeiculo(VeiculoDTO veiculoDTO)
        {
            Veiculo veiculo = new Veiculo();
            veiculo.FromVeiculoDto(veiculoDTO);

            _dataContext.Veiculo.Add(veiculo);
            _dataContext.SaveChangesAsync();

        }
    }
}
