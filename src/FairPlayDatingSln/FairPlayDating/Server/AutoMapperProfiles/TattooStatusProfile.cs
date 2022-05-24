using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.TattooStatus;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class TattooStatusProfile: Profile
    {
        public TattooStatusProfile()
        {
            CreateMap<TattooStatus, TattooStatusModel>();
        }
    }
}
