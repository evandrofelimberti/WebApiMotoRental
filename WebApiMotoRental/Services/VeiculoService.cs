using WebApiMotoRental.Data;
using WebApiMotoRental.DTO;
using WebApiMotoRental.Interfaces;
using WebApiMotoRental.Model;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace WebApiMotoRental.Services
{
    public class VeiculoService : VeiculoServiceImpl
    {
        private const string QUEUE_NAME = "cadastrar_veiculo";

        protected DataContext _dataContext;

        public VeiculoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void CadastrarVeiculo(VeiculoDTO veiculoDTO)
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: QUEUE_NAME,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            var stringFieldMessage = JsonConvert.SerializeObject(veiculoDTO);
            var bytesMessage = Encoding.UTF8.GetBytes(stringFieldMessage);

            channel.BasicPublish(exchange: "",
                                         routingKey: QUEUE_NAME,
                                         basicProperties: null,
                                         body: bytesMessage);

        }
    }
}
