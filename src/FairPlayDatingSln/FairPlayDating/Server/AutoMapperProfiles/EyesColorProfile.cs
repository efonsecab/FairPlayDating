using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.EyesColor;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class EyesColorProfile: Profile
    {
        public EyesColorProfile()
        {
            CreateMap<EyesColor, EyesColorModel>();
        }
    }
}
