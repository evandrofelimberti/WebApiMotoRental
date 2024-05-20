using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiMotoRental.DTO;
using WebApiMotoRental.Model;

namespace MotoRentalService
{
    public class VeiculoRepository : VeiculoRepositoryImpl
    {
        private readonly IDbContextFactory<DataContextService> _dbFactory;

        public VeiculoRepository(IDbContextFactory<DataContextService> dbContextFactory)
        {
            _dbFactory = dbContextFactory;
        }

        public void CadastrarVeiculo(VeiculoDTO veiculoDTO)
        {

            using (var context = _dbFactory.CreateDbContext())
            {
                Veiculo veiculo = new Veiculo();
                veiculo.FromVeiculoDto(veiculoDTO);

                context.Veiculo.Add(veiculo);
                context.SaveChangesAsync();
            }
        }
    }
}
