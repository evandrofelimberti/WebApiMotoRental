using Microsoft.EntityFrameworkCore;
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

        public void CadastrarVeiculo(VeiculoDTO veiculoDto)
        {

            using (var context = _dbFactory.CreateDbContext())
            {
                Veiculo veiculo = new Veiculo();
                veiculo.FromVeiculoDto(veiculoDto);

                context.Veiculo.Add(veiculo);
                context.SaveChangesAsync();
            }
        }
    }
}
