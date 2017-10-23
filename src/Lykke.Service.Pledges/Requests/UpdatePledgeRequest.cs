using Lykke.Service.Pledges.AzureRepositories.Entities;
using Lykke.Service.Pledges.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Lykke.Service.Pledges.Requests
{
    public class UpdatePledgeRequest : IPledge
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public int CO2Footprint { get; set; }
        [Required]
        public int ClimatePositiveValue { get; set; }        
    }
}
