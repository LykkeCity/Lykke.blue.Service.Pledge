using AzureStorage;
using Lykke.Service.Pledges.AzureRepositories.Entities;
using Lykke.Service.Pledges.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.AzureRepositories.Repositories
{
    public class PledgeRepository : IPledgeRepository
    {
        private readonly INoSQLTableStorage<PledgeEntity> _pledgeTableStorage;

        public PledgeRepository(INoSQLTableStorage<PledgeEntity> pledgeTableStorage)
        {
            _pledgeTableStorage = pledgeTableStorage;
        }

        public async Task<IPledge> Create(string clientId, int co2Footprint, int climatePositiveValue)
        {
            var newEntity = PledgeEntity.CreateNew(clientId, co2Footprint, climatePositiveValue);

            await _pledgeTableStorage.InsertAsync(newEntity);

            return newEntity;
        }

        public async Task Delete(string id)
        {
            var partitionKey = PledgeEntity.GeneratePartitionKey();
            var rowKey = PledgeEntity.GenerateRowKey(id);

            await _pledgeTableStorage.DeleteAsync(partitionKey, rowKey);
        }

        public async Task<IPledge> Get(string id)
        {
            var partitionKey = PledgeEntity.GeneratePartitionKey();
            var rowKey = PledgeEntity.GenerateRowKey(id);

            var entity = await _pledgeTableStorage.GetDataAsync(partitionKey, rowKey);

            return entity;
        }

        public async Task<IEnumerable<IPledge>> GetPledgesByClientId(string clientId)
        {
            var partitionKey = PledgeEntity.GeneratePartitionKey();

            var entities = await _pledgeTableStorage.GetDataAsync(partitionKey, x => x.ClientId == clientId);

            return entities;
        }

        public async Task<IPledge> UpdatePledge(IPledge pledge)
        {
            var partitionKey = PledgeEntity.GeneratePartitionKey();
            var rowKey = PledgeEntity.GenerateRowKey(pledge.Id);

            var entity = await _pledgeTableStorage.GetDataAsync(partitionKey, rowKey);

            PledgeEntity.Update(entity, pledge);

            await _pledgeTableStorage.InsertOrReplaceAsync(entity);

            return entity;
        }
    }
}
