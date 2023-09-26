using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Contracts;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Functions
{
    public class FunctionRabbitMQ
    {
        private readonly IProcessadorService _processadorService;
        private readonly ILogger<FunctionRabbitMQ> _logger;

        public FunctionRabbitMQ(IServiceProvider serviceProvider)
        {
            _processadorService = serviceProvider.GetRequiredService<IProcessadorService>();
            _logger = serviceProvider.GetRequiredService<ILogger<FunctionRabbitMQ>>();
        }
  

        [FunctionName(nameof(ConsumirEvento))]
        public async Task ConsumirEvento(
        [RabbitMQTrigger("%InputQueueName%", ConnectionStringSetting = "RabbitMQConnection")] string evento, string deliveryTag,
        [RabbitMQ(QueueName = "%OutputQueueName%", ConnectionStringSetting = "RabbitMQConnection")] IModel client)
        {
            try
            {
                _logger.LogInformation($"Evento recebido: {evento}");
                var ret = await _processadorService.ProcessarEvento(evento, deliveryTag);

                await RetornoProcessamento(ret.Message, client);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.Now:dd/MM/yyyyTHH:mm:ss}][{Thread.GetCurrentProcessorId():000000}] ERRO: {ex.Message}");
            }
        }


        public async Task RetornoProcessamento(string message, IModel client)
        {
            var _outputqueue = Environment.GetEnvironmentVariable("OutputQueueName");
            QueueDeclareOk queue = client.QueueDeclare(_outputqueue, true, false, false, null);

            var body = Encoding.UTF8.GetBytes(message);
            client.BasicPublish(exchange: "", routingKey: _outputqueue, basicProperties: null, body: body);
        }
    }
}
