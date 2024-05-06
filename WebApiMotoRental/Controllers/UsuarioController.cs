using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMotoRental.Data;
using WebApiMotoRental.Model;
using WebApiMotoRental.Services;


namespace WebAppKey.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly DataContext _context;

    public UsuarioController(DataContext context)
    {
        _context = context;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("auth")]
    public async Task<IActionResult> Auth(Usuario usuario)
    {
        try
        {

            var userExists =  await _context.Usuario.FirstOrDefaultAsync(u => u.Nome.ToLower() == usuario.Nome.ToLower());

            if (userExists == null)
                return BadRequest(new { Message = "Usuário e/ou senha está(ão) inválido(s)." });

            if (userExists.Senha != usuario.Senha)
                return BadRequest(new { Message = "Usuário e/ou senha está(ão) inválido(s)." });

            var token = UsuarioService.GenerateToken(userExists);

            userExists.Senha = "";

            return Ok(new
            {
                token = token,
                Usuario = userExists
            });

        }
        catch (Exception)
        {
            return BadRequest(new { Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente." });
        }
    }

}
