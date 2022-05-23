using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.Activity;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class ActivityProfile: Profile
    {
        public ActivityProfile()
        {
            this.CreateMap<Activity, ActivityModel>();
        }
    }
}
