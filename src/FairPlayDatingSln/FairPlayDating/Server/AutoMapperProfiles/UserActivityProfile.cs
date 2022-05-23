using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.UserActivity;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class UserActivityProfile: Profile
    {
        public UserActivityProfile()
        {
            CreateMap<UserActivity, UserActivityModel>();
            CreateMap<UserActivityModel, UserActivity>();
        }
    }
}
