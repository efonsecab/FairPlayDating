using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.DateObjective;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class DateObjectiveProfile: Profile
    {
        public DateObjectiveProfile()
        {
            CreateMap<DateObjective, DateObjectiveModel>();
        }
    }
}
