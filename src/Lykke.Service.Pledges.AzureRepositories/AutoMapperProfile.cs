using AutoMapper;
using Lykke.Service.Pledges.AzureRepositories.DTOs;
using Lykke.Service.Pledges.AzureRepositories.Entities;
using Lykke.Service.Pledges.Core.Domain;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges.AzureRepositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //To entities
            CreateMap<IPledge, PledgeEntity>();

            ForAllMaps((map, cfg) =>
            {
                if (map.DestinationType.IsSubclassOf(typeof(TableEntity)))
                {
                    cfg.ForMember("ETag", opt => opt.Ignore());
                    cfg.ForMember("PartitionKey", opt => opt.Ignore());
                    cfg.ForMember("RowKey", opt => opt.Ignore());
                    cfg.ForMember("Timestamp", opt => opt.Ignore());
                }
            });

            //From entities
            CreateMap<PledgeEntity, PledgeDto>();
        }
    }
}
