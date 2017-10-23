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
        Task<IEnumerable<IPledge>> GetPledgesByClientId(string id);
        Task<IPledge> Update(IPledge pledge);
        Task Delete(string id);
    }
}
