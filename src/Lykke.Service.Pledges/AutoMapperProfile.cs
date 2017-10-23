using AutoMapper;
using Lykke.Service.Pledges.AzureRepositories.Entities;
using Lykke.Service.Pledges.Core.Domain;
using Lykke.Service.Pledges.Requests;
using Lykke.Service.Pledges.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.Pledges
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IPledge, CreatePledgeRequest>();
            CreateMap<IPledge, GetPledgeResponse>();
            CreateMap<IPledge, UpdatePledgeRequest>();
            CreateMap<IPledge, UpdatePledgeResponse>();
        }
    }
}
