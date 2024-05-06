using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMotoRental.Data;
using WebApiMotoRental.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiMotoRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        protected DataContext _Context;

        public PessoaController(DataContext context)
        {
            _Context = context;
        }

        // GET: api/<PessoaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> Get()
        {
            return await _Context.Pessoa.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> Get(int id)
        {
            var pessoa = await _Context.Pessoa.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (pessoa == null)
            {
                return NotFound();
            }
            return pessoa;

        }

        // POST api/<PessoaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pessoa pessoa)
        {
            _Context.Pessoa.Add(pessoa);
            await _Context.SaveChangesAsync();
            return CreatedAtAction(nameof(Post), new { id = pessoa.Id }, pessoa);

        }

        // PUT api/<PessoaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Pessoa pessoa)
        {
            if (pessoa.Id != id)
            {
                BadRequest();
            }

            _Context.Entry(pessoa).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<PessoaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _Context.Pessoa.FindAsync(id);
            if(pessoa == null)
            {
                return NotFound();
            }
            _Context.Pessoa.Remove(pessoa);
            await _Context.SaveChangesAsync();
            return NoContent();
        }
    }
}
