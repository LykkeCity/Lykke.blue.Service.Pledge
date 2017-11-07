using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Core.Domain
{
    public interface IPledgeRepository
    {
        Task<string> Create(IPledge pledge);
        Task<IPledge> Get(string id);
        Task<IPledge> GetPledgeByClientId(string clientId);
        Task UpdatePledge(IPledge pledge);
        Task Delete(string id);
        Task<bool> IsPledgesLimitReached(string clientId);
    }
}
