using AutoMapper;
using Lykke.Service.Pledges.Core.Domain;
using Lykke.Service.Pledges.Requests;
using Lykke.Service.Pledges.Responses;

namespace Lykke.Service.Pledges
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IPledge, CreatePledgeRequest>();
            CreateMap<IPledge, CreatePledgeResponse>();
            CreateMap<IPledge, GetPledgeResponse>();
            CreateMap<IPledge, UpdatePledgeRequest>();
            CreateMap<IPledge, UpdatePledgeResponse>();
        }
    }
}
