using Lykke.Service.Pledges.Core.Domain;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Core.Services
{
    public interface IPledgesService
    {
        Task Create(IPledge pledge);
        Task<IPledge> Get(string id);
        Task<IPledge> GetPledgeByClientId(string clientId);
        Task Update(IPledge pledge);
        Task Delete(string id);
        Task<bool> IsPledgesLimitReached(string clientId);
    }
}
