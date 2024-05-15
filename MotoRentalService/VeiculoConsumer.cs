using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApiMotoRental.DTO;
using Newtonsoft.Json;
using WebApiMotoRental.Data;
using WebApiMotoRental.Model;

namespace MotoRentalService
{
    public class VeiculoConsumer : BackgroundService
    {
        private const string QUEUE_NAME = "cadastrar_veiculo";
        private readonly ILogger<VeiculoConsumer> _logger;
        private readonly int _intervaloMensagemWorkerAtivo;
        //private readonly DataContext _context;
        private readonly VeiculoRepositoryImpl veiculoRepository;

        public VeiculoConsumer(VeiculoRepositoryImpl veiculoRepository)
        {
            this.veiculoRepository = veiculoRepository;
        }

        /* public VeiculoConsumer(ILogger<VeiculoConsumer> logger, 
              IConfiguration configuration, DataContext context)
          {
              _logger = logger;
              _intervaloMensagemWorkerAtivo = 10000; // 10 segundos
              _context = context;
          }*/

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: QUEUE_NAME,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += consumer_received;
            var consumerTag = channel.BasicConsume(QUEUE_NAME, true, consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
               /* _logger.LogInformation(
                    $"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");*/
                await Task.Delay(10000, stoppingToken);
            }
        }

        public void GravarLog(VeiculoDTO veiculoDTO)
        {
            this.veiculoRepository.CadastrarVeiculo(veiculoDTO);

           /* if (veiculoDTO.Placa.Any())
            {
                Veiculo veiculo = new Veiculo();
                veiculo.FromVeiculoDto(veiculoDTO);

                _context.Veiculo.Add(veiculo);
                _context.SaveChangesAsync();
                _logger.LogInformation($"Veiculo cadastrado! \n " +
                    $"Placa: {veiculoDTO.Placa} \n Modelo {veiculoDTO.Modelo}");
            }*/
        }

        private void consumer_received(
                    object sender, BasicDeliverEventArgs eventArgs)
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            VeiculoDTO veiculoDTO = JsonConvert.DeserializeObject<VeiculoDTO>(contentString);
            if (veiculoDTO != null)
                GravarLog(veiculoDTO);
        }
    }
}