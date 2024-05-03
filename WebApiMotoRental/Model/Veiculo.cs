using System.ComponentModel.DataAnnotations;

namespace WebApiMotoRental.Model
{
    public class Veiculo
    {
        [Required]
        [Key]
        public int Id { get; set; }

       // [Index(IsUnique = true)]
        public string Placa { get; set; } = string.Empty;

        public string Ano { get; set; } = string.Empty;

        public string Modelo { get; set; } = string.Empty;
    }
}
