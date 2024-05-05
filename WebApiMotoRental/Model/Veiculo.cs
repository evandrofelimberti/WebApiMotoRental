
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApiMotoRental.Model
{
    [Index(nameof(Placa), IsUnique = true)]
    public class Veiculo
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Placa { get; set; } = string.Empty;

        public string Ano { get; set; } = string.Empty;

        public string Modelo { get; set; } = string.Empty;
    }
}
