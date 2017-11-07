using AutoMapper;
using AzureStorage;
using Lykke.Service.Pledges.AzureRepositories.DTOs;
using Lykke.Service.Pledges.AzureRepositories.Entities;
using Lykke.Service.Pledges.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.Pledges.AzureRepositories.Repositories
{
    public class PledgeRepository : IPledgeRepository
    {
        private readonly INoSQLTableStorage<PledgeEntity> _pledgeTable;

        public PledgeRepository(INoSQLTableStorage<PledgeEntity> pledgeTableStorage)
        {
            _pledgeTable = pledgeTableStorage;
        }

        public static string GetPartitionKey() => "Pledge";

        public static string GetRowKey(string id)
        {
            if (String.IsNullOrEmpty(id))
                return Guid.NewGuid().ToString();

            return id;
        }

        public async Task<string> Create(IPledge pledge)
        {
            var entity = Mapper.Map<PledgeEntity>(pledge);

            entity.PartitionKey = GetPartitionKey();
            entity.RowKey = GetRowKey(pledge.Id);

            await _pledgeTable.InsertAsync(entity);

            return entity.ClientId;
        }

        public async Task Delete(string id)
        {
            await _pledgeTable.DeleteAsync(GetPartitionKey(), GetRowKey(id));
        }

        public async Task<IPledge> Get(string id)
        {
            var entity = await _pledgeTable.GetDataAsync(GetPartitionKey(), GetRowKey(id));

            return Mapper.Map<PledgeDto>(entity);
        }

        public async Task<IPledge> GetPledgeByClientId(string clientId)
        {
            var entities = (await _pledgeTable.GetDataAsync(GetPartitionKey(), x => x.ClientId == clientId)).FirstOrDefault();

            return Mapper.Map<PledgeDto>(entities);
        }

        public async Task<bool> IsPledgesLimitReached(string clientId)
        {
            var numberOfClientPledges = (await _pledgeTable.GetDataAsync(GetPartitionKey(), x => x.ClientId == clientId)).Count();

            return numberOfClientPledges >= 1;
        }

        public async Task UpdatePledge(IPledge pledge)
        {
            var result = await _pledgeTable.MergeAsync(GetPartitionKey(), GetRowKey(pledge.Id), x =>
            {
                Mapper.Map(pledge, x);

                return x;
            });
        }
    }
}
