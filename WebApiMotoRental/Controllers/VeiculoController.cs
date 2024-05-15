using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetAll()
        {
            return await _Context.Veiculo.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Veiculo>> GetByPlaca(string placa)
        {
            if (string.IsNullOrEmpty(placa)) return NotFound();

            var veiculo = await _Context.Veiculo.Where(v => v.Placa == placa).FirstOrDefaultAsync();
            if (veiculo == null)
            {
                return NotFound();
            }

            return veiculo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void Post(VeiculoDTO veiculoDTO)
        {
            VeiculoService veiculoService = new VeiculoService(_Context);
            veiculoService.CadastrarVeiculo(veiculoDTO);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutPlaca(int id, string placa)
        {
            // Validar se existe locação com esse veiculo, caso existir não permitir alterar a placa.
            var veiculo = await _Context.Veiculo.FindAsync(id);
            if (veiculo == null ||  string.IsNullOrEmpty(placa))
            {
                return NotFound();
            }
            veiculo.Placa = placa;

            _Context.Entry(veiculo).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void Delete(int id)
        {
            Veiculo veiculo = _Context.Veiculo.Where(v => v.Id == id).First();
            if (veiculo != null)
            {
                _Context.Veiculo.Remove(veiculo);
                _Context.SaveChangesAsync();
            }                     
        }
    }
}
