
using Lykke.Service.Pledges.Client.AutorestClient.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Client
{
    public interface IPledgesClient
    {
        Task<CreatePledgeResponse> Create(CreatePledgeRequest request);
        Task<GetPledgeResponse> Get(string id);
        Task<GetPledgeResponse> GetPledgeByClientId(string id);
        Task<UpdatePledgeResponse> Update(UpdatePledgeRequest request);
        Task Delete(string id);
    }
}
