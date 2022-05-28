using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.UserPhoto;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class UserPhotoProfile : Profile
    {
        public UserPhotoProfile()
        {
            CreateMap<UserPhoto, UserPhotoModel>();
            CreateMap<UserPhotoModel, UserPhoto>();
        }
    }
}
