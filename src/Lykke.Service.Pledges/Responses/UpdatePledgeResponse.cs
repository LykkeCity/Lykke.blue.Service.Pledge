using Lykke.Service.Pledges.Core.Domain;

namespace Lykke.Service.Pledges.Responses
{
    public class UpdatePledgeResponse : IPledge
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public int CO2Footprint { get; set; }
        public int ClimatePositiveValue { get; set; }
    }
}
