using AzureStorage.Tables;
using Common.Log;
using Lykke.Service.Pledges.AzureRepositories.Entities;
using Lykke.Service.Pledges.AzureRepositories.Repositories;
using Lykke.SettingsReader;

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
