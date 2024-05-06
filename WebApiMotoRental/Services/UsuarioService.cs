using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiMotoRental.Enum;
using WebApiMotoRental.Model;

namespace WebApiMotoRental.Services
{
    public class UsuarioService
    {
        public static string GenerateToken(Usuario usuario)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, usuario.Nome.ToString()),
                new Claim(ClaimTypes.Role, RoleFactory(usuario.TipoUsuario))
                }),
                Expires = DateTime.UtcNow.AddDays(30),

                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static string RoleFactory(eTipoUsuario tipoUsuario)
        {
            switch (tipoUsuario)
            {
                case eTipoUsuario.tuAdmin:
                    return "Admin";
                case eTipoUsuario.tuEntregador:
                    return "Entregador";
                default:
                    throw new Exception();
            }
        }
    }
}
