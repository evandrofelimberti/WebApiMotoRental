using Microsoft.EntityFrameworkCore;

namespace WebApiMotoRental.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
        

    }
}
