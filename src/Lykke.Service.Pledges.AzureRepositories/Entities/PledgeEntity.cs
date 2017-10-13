using Lykke.Service.Pledges.Core.Domain;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges.AzureRepositories.Entities
{
    public class PledgeEntity : TableEntity, IPledge
    {
        #region Properties

        public string Id => RowKey;

        public string ClientId { get; set; }

        public int CO2Footprint { get; set; }

        public int ClimatePositiveValue { get; set; }

        #endregion

        #region Public methods

        public static IEqualityComparer<PledgeEntity> ComparerById { get; } = new EqualityComparerById();

        public static string GeneratePartitionKey()
        {
            return "Pledge";
        }

        public static string GenerateRowKey(string id)
        {
            return id;
        }

        public static PledgeEntity CreateNew(string clientId, int co2Footprint, int pledgeValue)
        {
            var result = new PledgeEntity
            {
                ClientId = clientId,
                CO2Footprint = co2Footprint,
                PartitionKey = GeneratePartitionKey(),
                RowKey = Guid.NewGuid().ToString(),
                ClimatePositiveValue = pledgeValue
            };

            return result;
        }

        public static PledgeEntity Update(IPledge from, IPledge to)
        {
            from.ClimatePositiveValue = to.ClimatePositiveValue;
            from.CO2Footprint = to.CO2Footprint;

            return CreateNew(from.ClientId, from.CO2Footprint, from.ClimatePositiveValue);
        }

        #endregion

        #region Private methods

        private class EqualityComparerById : IEqualityComparer<PledgeEntity>
        {
            public bool Equals(PledgeEntity x, PledgeEntity y)
            {
                if (x == y)
                    return true;
                if (x == null || y == null)
                    return false;
                return x.Id == y.Id;
            }

            public int GetHashCode(PledgeEntity obj)
            {
                if (obj?.Id == null)
                    return 0;
                return obj.Id.GetHashCode();
            }
        }

        #endregion
    }
}
