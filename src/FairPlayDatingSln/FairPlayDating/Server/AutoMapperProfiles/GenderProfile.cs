using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.Gender;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<Gender, GenderModel>();
        }
    }
}
