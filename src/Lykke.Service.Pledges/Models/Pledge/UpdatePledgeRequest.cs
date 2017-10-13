using Lykke.Service.Pledges.AzureRepositories.Entities;
using Lykke.Service.Pledges.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lykke.Service.Pledges.Models.Pledge
{
    public class UpdatePledgeRequest
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public int CO2Footprint { get; set; }
        [Required]
        public int ClimatePositiveValue { get; set; }

        public IPledge Create(string id)
        {
            return new PledgeEntity
            {
                RowKey = id,
                ClientId = this.ClientId,
                ClimatePositiveValue = this.ClimatePositiveValue,
                CO2Footprint = this.CO2Footprint
            };
        }
    }
}
