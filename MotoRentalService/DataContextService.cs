using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiMotoRental.Model;

namespace MotoRentalService
{
    public class DataContextService : DbContext
    {
        public DataContextService(DbContextOptions<DataContextService> options) : base(options)
        {

        }
        public DbSet<Veiculo> Veiculo { get; set; } = null!;
    }
}
