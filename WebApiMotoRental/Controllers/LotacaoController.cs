using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMotoRental.Data;
using WebApiMotoRental.DTO;
using WebApiMotoRental.Model;
using WebApiMotoRental.Services;

namespace WebApiMotoRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotacaoController : ControllerBase
    {
        protected DataContext _Context;

        public LotacaoController(DataContext context)
        {
            _Context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Entregador")]
        public async Task<ActionResult<IEnumerable<Locacao>>> GetAll()
        {
            return await _Context.Locacao.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Entregador")]
        public async Task<ActionResult<Locacao>> GetById(int id)
        {
            if (!(id > 0)) return NotFound();

            var locacao = await _Context.Locacao.FindAsync(id);
            if (locacao == null)
            {
                return NotFound();
            }

            return locacao;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Entregador")]
        public void Post(LocacaoDTO locacaoDto)
        {
            LocacaoService locacaoService = new LocacaoService(_Context);
            locacaoService.CadastrarLocacao(locacaoDto);

        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Entregador")]
        public void DevolverVeiculo(int id, LocacaoDTO locacaoDto)
        {            
            LocacaoService locacaoService = new LocacaoService(_Context);
            locacaoService.DevolverVeiculo(id, locacaoDto);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void Delete(int id)
        {
            Locacao locacao = _Context.Locacao.Where(v => v.Id == id).First();
            if (locacao != null)
            {
                _Context.Locacao.Remove(locacao);
                _Context.SaveChangesAsync();
            }
        }
    }
}
