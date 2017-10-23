using Lykke.Service.Pledges.Core.Domain;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Lykke.Service.Pledges.Requests
{
    public class CreatePledgeRequest : IPledge
    {
        //REMARK: We do not need to allow someone to set Id. Id is set automatically.
        [IgnoreDataMember]
        public string Id { get; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public int CO2Footprint { get; set; }
        [Required]
        public int ClimatePositiveValue { get; set; }        
    }
}
