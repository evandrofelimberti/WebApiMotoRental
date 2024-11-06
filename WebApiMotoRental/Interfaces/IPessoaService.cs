using WebApiMotoRental.Model;

namespace WebApiMotoRental.Interfaces
{
    public interface PessoaServiceImpl
    {
        IEnumerable<Pessoa> GetAll();
        Pessoa GetById(int id);
    }
}
