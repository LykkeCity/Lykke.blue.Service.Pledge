
using Lykke.Service.Pledges.Client.AutorestClient.Models;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Client
{
    public interface IPledgesClient
    {
        Task Create(CreatePledgeRequest request);
        Task<GetPledgeResponse> Get(string clientId);
        Task Update(UpdatePledgeRequest request);
        Task Delete(string id);
    }
}
