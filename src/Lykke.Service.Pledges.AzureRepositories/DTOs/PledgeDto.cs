using Lykke.Service.Pledges.Core.Domain;

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
