using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.UserProfile;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class UserProfileProfile: Profile
    {
        public UserProfileProfile()
        {
            CreateMap<UserProfile, UserProfileModel>();
            CreateMap<UserProfileModel, UserProfile>();
        }
    }
}
