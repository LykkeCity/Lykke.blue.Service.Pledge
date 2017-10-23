using Lykke.Service.Pledges.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges.AzureRepositories.DTOs
{
    public class PledgeDto : IPledge
    {
        public string Id { get; set; }

        public string ClientId { get; set; }

        public int CO2Footprint { get; set; }

        public int ClimatePositiveValue { get; set; }
    }
}
