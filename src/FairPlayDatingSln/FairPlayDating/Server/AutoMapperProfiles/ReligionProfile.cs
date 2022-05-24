using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.Religion;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class ReligionProfile: Profile
    {
        public ReligionProfile()
        {
            CreateMap<Religion, ReligionModel>();
        }
    }
}
