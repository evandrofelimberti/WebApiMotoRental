using Microsoft.EntityFrameworkCore;
using WebApiMotoRental.Model;

namespace WebApiMotoRental.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; } = null!;
        public DbSet<Veiculo> Veiculo { get; set; } = null!;
        public DbSet<Pessoa> Pessoa { get; set; } = null!;
        public DbSet<PessoaDocumento> PessoaDocumento { get; set; } = null!;
        public DbSet<PessoaDocumentoCNH> PessoaDocumentoCNH { get; set; } = null!;
        public DbSet<PessoaDocumentoTipoCNH> PessoaDocumentoTipoCNH { get; set; } = null!;
        public DbSet<Locacao> Locacao { get; set; } = null!;
        public DbSet<PlanoLocacao> PlanoLocacao { get; set; } = null!;

    }
}
