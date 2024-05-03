using System.ComponentModel.DataAnnotations;
using WebApiMotoRental.Enum;

namespace WebApiMotoRental.Model
{
    public class Usuario
    {
        [Required]
        [Key]
        public int Id { get; set; } 

        public string Nome { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public eTipoUsuario TipoUsuario { get; set; } = eTipoUsuario.tuNenhum;

    }
}
