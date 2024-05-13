namespace WebApiMotoRental.DTO
{
    public class VeiculoDTO
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Ano { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
    }
}
