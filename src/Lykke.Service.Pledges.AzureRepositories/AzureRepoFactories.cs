using AzureStorage.Tables;
using Common.Log;
using Lykke.Service.Pledges.AzureRepositories.Entities;
using Lykke.Service.Pledges.AzureRepositories.Repositories;
using Lykke.SettingsReader;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges.AzureRepositories
{
    public static class AzureRepoFactories
    {
        public static PledgeRepository CreatePledgeRepository(IReloadingManager<string> connString, ILog log)
        {
            return new PledgeRepository(AzureTableStorage<PledgeEntity>.Create(connString, "Pledges", log));
        }
    }
}
