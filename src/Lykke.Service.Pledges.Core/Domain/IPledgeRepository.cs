﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.Core.Domain
{
    public interface IPledgeRepository
    {
        Task<IPledge> Create(string clientId, int co2Footprint, int value);
        Task<IPledge> Get(string id);
        Task<IEnumerable<IPledge>> GetPledgesByClientId(string clientId);
        Task<IPledge> UpdatePledge(IPledge pledge);
        Task Delete(string id);
    }
}