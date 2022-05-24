using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.KidStatus;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class KidStatusProfile : Profile
    {
        public KidStatusProfile()
        {
            CreateMap<KidStatus, KidStatusModel>();
        }
    }
}
