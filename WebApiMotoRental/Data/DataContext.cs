using Microsoft.EntityFrameworkCore;
using WebApiMotoRental.Model;

namespace WebApiMotoRental.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Usuario> Unidade { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<PessoaDocumento> PessoaDocumento { get; set; }
        public DbSet<PessoaDocumentoCNH> PessoaDocumentoCNH { get; set; }
        public DbSet<PessoaDocumentoTipoCNH> PessoaDocumentoTipoCNH { get; set; }

    }
}
