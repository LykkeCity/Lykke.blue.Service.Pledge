using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges.Core.Domain
{
    public interface IPledgeStatistics
    {
        int CurrentProgress { get; }
        int CommitmentGiven { get; }
    }
}
