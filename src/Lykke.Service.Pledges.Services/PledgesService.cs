using Lykke.Service.Pledges.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.Pledges.Core.Domain;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Services
{
    public class PledgesService : IPledgesService
    {
        private readonly IPledgeRepository _pledgeRepository;

        public PledgesService(IPledgeRepository pledgeRepository)
        {
            _pledgeRepository = pledgeRepository;
        }

        public async Task<IPledge> Create(IPledge pledge)
        {
            return await _pledgeRepository.Create(pledge);
        }

        public async Task Delete(string id)
        {
            await _pledgeRepository.Delete(id);
        }

        public async Task<IPledge> Get(string id)
        {
            return await _pledgeRepository.Get(id);
        }

        public async Task<IPledge> GetPledgeByClientId(string clientId)
        {
            return await _pledgeRepository.GetPledgeByClientId(clientId);
        }

        public async Task<bool> IsPledgesLimitReached(string clientId)
        {
            return await _pledgeRepository.IsPledgesLimitReached(clientId);
        }

        public async Task<IPledge> Update(IPledge pledge)
        {
            return await _pledgeRepository.UpdatePledge(pledge);
        }
    }
}
