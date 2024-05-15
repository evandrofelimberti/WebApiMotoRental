using WebApiMotoRental.Data;
using WebApiMotoRental.DTO;
using WebApiMotoRental.Enum;
using WebApiMotoRental.Interfaces;
using WebApiMotoRental.Model;

namespace WebApiMotoRental.Services
{
    public class PessoaService : PessoaServiceImpl
    {
        protected DataContext _dataContext;

        public PessoaService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _dataContext.Pessoa.ToList();
        }

        public Pessoa GetById(int id)
        {
            var pessoa = _dataContext.Pessoa.Find(id);
            if (pessoa == null)
                throw new Exception("Pessoa não localizada!!!");

            return pessoa;
        }

        private bool ExisteDocumentoNumeroTipoCadastrado(string numero, eTipoPessoaDocumento tipo)
        {
            var possuiNumeroDocumento = _dataContext.PessoaDocumento
                .Where(n => n.Numero.ToUpper().Contains(numero.ToUpper()) && n.Tipo == tipo);

            return possuiNumeroDocumento.Count() > 0;
        }

        public bool ExisteDocumentoNumeroTipoCadastrado(PessoaDTO pessoaDTO)
        {
            foreach (var item in pessoaDTO.PessoaDocumento)
            {
                if (ExisteDocumentoNumeroTipoCadastrado(item.Numero, item.Tipo))
                {
                    return true;
                    //throw new Exception($"Numero de documento já cadastro!!! \n Numero {item.Numero}; Tipo {item.Tipo}");
                }
            }
            return false;
        }

        public ValidacaoPessoaResultado ValidarCadastroPessoa(PessoaDTO pessoaDTO)
        {
            if (pessoaDTO == null)
            {
                throw new ArgumentNullException("pessoaDTO");
            }

            var result = ValidacaoPessoaResultado.Default;

            if (ExisteDocumentoNumeroTipoCadastrado(pessoaDTO))
            {
                result |= (ValidacaoPessoaResultado.NumeroDocumentoCadastrado);
            }

            if(!result.HasFlag(ValidacaoPessoaResultado.NumeroDocumentoCadastrado) ||
               !result.HasFlag(ValidacaoPessoaResultado.TipoDocumentoNaoInformado))
            {
                result |= ValidacaoPessoaResultado.Ok;
            }

            return result;

        }
    }
}
