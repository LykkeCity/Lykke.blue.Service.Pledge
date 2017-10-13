using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Core.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();
    }
}