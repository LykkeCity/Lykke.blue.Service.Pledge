using Lykke.Service.Pledges.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges.Models.Pledge
{
    public class CreatePledgeResponse
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public int CO2Footprint { get; set; }
        public int ClimatePositiveValue { get; set; }

        public static CreatePledgeResponse Create(IPledge pledge)
        {
            return new CreatePledgeResponse
            {
                Id = pledge.Id,
                ClientId = pledge.ClientId,
                CO2Footprint = pledge.CO2Footprint,
                ClimatePositiveValue = pledge.ClimatePositiveValue
            };
        }
    }
}
