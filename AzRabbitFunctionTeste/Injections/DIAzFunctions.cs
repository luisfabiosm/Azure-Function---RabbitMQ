
using Domain.Core.Contracts;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Registers
{
    public static class FunctionInjections
    {

        public static IServiceCollection AddFunctionInjections(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IProcessadorService, ProcessadorService>();
            service.AddLogging();

            return service;
        }
    }
}
