using Microsoft.AspNetCore.Mvc;
using WebApiMotoRental.Data;
using WebApiMotoRental.DTO;
using WebApiMotoRental.Model;
using WebApiMotoRental.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiMotoRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        protected DataContext _Context;

        public VeiculoController(DataContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public ActionResult<Veiculo> Get(int id)
        {
            return _Context.Veiculo.Find(id);
        }

        [HttpPost]
        public void Post(VeiculoDTO veiculoDTO)
        {
            VeiculoService veiculoService = new VeiculoService(_Context);
            veiculoService.CadastrarVeiculo(veiculoDTO);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
