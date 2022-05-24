using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.HairColor;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class HairColorProfile: Profile
    {
        public HairColorProfile()
        {
            CreateMap<HairColor, HairColorModel>();;
        }
    }
}
