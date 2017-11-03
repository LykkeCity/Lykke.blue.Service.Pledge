using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Core.Domain
{
    public interface IPledgeRepository
    {
        Task<IPledge> Create(IPledge pledge);
        Task<IPledge> Get(string id);
        Task<IPledge> GetPledgeByClientId(string clientId);
        Task<IPledge> UpdatePledge(IPledge pledge);
        Task Delete(string id);
        Task<bool> IsPledgesLimitReached(string clientId);
        Task<IPledgeStatistics> GetPledgeStatistics(string id);
    }
}
