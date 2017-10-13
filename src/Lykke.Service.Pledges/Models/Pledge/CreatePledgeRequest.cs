using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lykke.Service.Pledges.Models.Pledge
{
    public class CreatePledgeRequest
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public int CO2Footprint { get; set; }
        [Required]
        public int ClimatePositiveValue { get; set; }
    }
}
