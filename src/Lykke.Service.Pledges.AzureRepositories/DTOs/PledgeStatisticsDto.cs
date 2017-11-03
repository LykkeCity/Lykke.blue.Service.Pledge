using Lykke.Service.Pledges.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges.AzureRepositories.DTOs
{
    public class PledgeStatisticsDto : IPledgeStatistics
    {
        public int CurrentProgress { get; set; }

        public int CommitmentGiven { get; set; }
    }
}
