﻿using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.Match;

namespace FairPlayDating.Server.AutoMapperProfiles
{
    public class MatchProfile: Profile
    {
        public MatchProfile()
        {
            CreateMap<UserProfile, MatchModel>();
        }
    }
}
