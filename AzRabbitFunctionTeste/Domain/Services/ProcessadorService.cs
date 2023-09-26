using Domain.Core.Contracts;
using Domain.Core.Model;
using Domain.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ProcessadorService : IProcessadorService
    {
        private readonly ILogger<ProcessadorService> _logger;
        public ProcessadorService(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<ProcessadorService>>();
        }
        public async Task<BaseReturn> ProcessarEvento(string evento, string id)
        {
            try
            {
                var _evento = JsonSerializer.Deserialize<Evento>(evento);

                _logger.LogInformation($"Inicar tratamento do evento : {evento}");
                _evento.ReqID = id;
                _logger.LogInformation($"Evento processador com ReqId : {id}");

                return new BaseReturn().returnSucess(_evento);
            }
            catch (Exception ex)
            {
                return new BaseReturn().returnError(ex);
            }

        }
    }
}
