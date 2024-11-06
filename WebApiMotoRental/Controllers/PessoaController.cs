using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMotoRental.Data;
using WebApiMotoRental.DTO;
using WebApiMotoRental.Enum;
using WebApiMotoRental.Model;
using WebApiMotoRental.Services;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> Get()
        {
           /* return await _Context.Pessoa
                .Include(d => d.PessoaDocumento)
                .ThenInclude(c => c.PessoaDocumentoCNH)
                .ThenInclude(t => t.PessoaDocumentoTipoCNH)
                .ToListAsync();*/
           return await _Context.Pessoa.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> Get(int id)
        {
           /* var pessoa = await _Context.Pessoa.Where(p => p.Id == id)
                .Include(d => d.PessoaDocumento)
                .ThenInclude(c => c.PessoaDocumentoCNH)
                .ThenInclude(t => t.PessoaDocumentoTipoCNH)
                .ToListAsync();*/
            var pessoa = await _Context.Pessoa.FindAsync(id);
            
            if (pessoa == null)
            {
                return NotFound();
            }
            return pessoa;

        }

        [HttpPost]
        [Authorize(Roles = "Admin,Entregador")]
        public async Task<IActionResult> Post(PessoaDTO pessoaDto)
        {
            PessoaService pessoaService = new PessoaService(_Context);
            var resultValidacao = pessoaService.ValidarCadastroPessoa(pessoaDto);
            if ((resultValidacao & ValidacaoPessoaResultado.Ok) == ValidacaoPessoaResultado.Ok)
            {
                Pessoa pessoa = new Pessoa();
                pessoa.FromPessoaDTO(pessoaDto);

                _Context.Pessoa.Add(pessoa);
                await _Context.SaveChangesAsync();
                return CreatedAtAction(nameof(Post), new { id = pessoa.Id }, pessoa);
            }
            return BadRequest();         
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Entregador")]
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
