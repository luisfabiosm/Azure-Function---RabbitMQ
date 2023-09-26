using Domain.Core.Model;
using Domain.Core.Models;
using System.Threading.Tasks;

namespace Domain.Core.Contracts
{
    public interface IProcessadorService
    {
        Task<BaseReturn> ProcessarEvento(string evento, string id);

    }
}
