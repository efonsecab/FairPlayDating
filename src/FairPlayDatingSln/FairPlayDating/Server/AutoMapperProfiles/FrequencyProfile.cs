using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.Frequency;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class FrequencyProfile: Profile
    {
        public FrequencyProfile()
        {
            CreateMap<Frequency, FrequencyModel>();
        }
    }
}
