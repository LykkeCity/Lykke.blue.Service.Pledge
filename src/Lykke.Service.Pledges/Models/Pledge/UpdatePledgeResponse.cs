using Lykke.Service.Pledges.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges.Models.Pledge
{
    public class UpdatePledgeResponse
    {
        public string Id { get; set; }
        public int CO2Footprint { get; set; }
        public int ClimatePositiveValue { get; set; }

        public static UpdatePledgeResponse Create(IPledge pledge)
        {
            return new UpdatePledgeResponse
            {
                Id = pledge.Id,
                CO2Footprint = pledge.CO2Footprint,
                ClimatePositiveValue = pledge.ClimatePositiveValue
            };
        }
    }
}
