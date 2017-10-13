using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges.Core.Domain
{
    public interface IPledge
    {
        string Id { get; }
        string ClientId { get; set; }
        int CO2Footprint { get; set; }
        int ClimatePositiveValue { get; set; }
    }
}
