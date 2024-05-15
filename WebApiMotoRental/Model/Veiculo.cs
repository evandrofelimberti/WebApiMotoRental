
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApiMotoRental.DTO;

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

        public void FromVeiculoDto(VeiculoDTO veiculoDto)
        {
            if (veiculoDto is null)
            {
                throw new ArgumentNullException(nameof(veiculoDto));
            }

            if (veiculoDto.Id > 0)
            {
                this.Id = veiculoDto.Id;
            }
            this.Placa = veiculoDto.Placa;
            this.Ano = veiculoDto.Ano;
            this.Modelo = veiculoDto.Modelo;
        }
    }
}
