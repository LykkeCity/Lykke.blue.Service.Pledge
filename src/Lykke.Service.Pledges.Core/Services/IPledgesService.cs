using Lykke.Service.Pledges.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Core.Services
{
    public interface IPledgesService
    {
        Task<IPledge> Create(IPledge pledge);
        Task<IPledge> Get(string id);
        Task<IPledge> GetPledgeByClientId(string clientId);
        Task<IPledge> Update(IPledge pledge);
        Task Delete(string id);
        Task<bool> IsPledgesLimitReached(string clientId);
        Task<IPledgeStatistics> GetPledgeStatistics(string id);
    }
}
