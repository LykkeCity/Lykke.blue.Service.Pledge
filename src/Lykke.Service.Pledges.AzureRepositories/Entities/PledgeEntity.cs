using Lykke.Service.Pledges.Core.Domain;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges.AzureRepositories.Entities
{
    public class PledgeEntity : TableEntity
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
