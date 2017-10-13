using Lykke.Service.Pledges.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges.Models.Pledge
{
    public class GetPledgeResponse
    {
        public string Id { get; set; }

        public string ClientId { get; set; }

        public int CO2Footprint { get; set; }

        public int ClimatePositiveValue { get; set; }

        public static GetPledgeResponse Create(IPledge pledge)
        {
            return new GetPledgeResponse
            {
                Id = pledge.Id,
                ClientId = pledge.ClientId,
                ClimatePositiveValue = pledge.ClimatePositiveValue,
                CO2Footprint = pledge.CO2Footprint
            };
        }
    }
}
