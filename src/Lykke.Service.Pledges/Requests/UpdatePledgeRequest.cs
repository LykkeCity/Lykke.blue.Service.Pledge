using Lykke.Service.Pledges.Core.Domain;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Lykke.Service.Pledges.Requests
{
    public class UpdatePledgeRequest : IPledge
    {
        [IgnoreDataMember]
        public string Id { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public int CO2Footprint { get; set; }
        [Required]
        public int ClimatePositiveValue { get; set; }
    }
}
