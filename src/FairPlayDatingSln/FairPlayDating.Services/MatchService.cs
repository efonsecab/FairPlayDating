using FairPlayDating.Common.Interfaces;
using FairPlayDating.DataAccess.Data;
using FairPlayDating.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Services
{
    public class MatchService
    {
        private FairPlayDatingDbContext _fairPlayDatingDbContext;
        private ICurrentUserProvider _currentUserProvider;

        public MatchService(FairPlayDatingDbContext fairPlayDatingDbContext,
            ICurrentUserProvider currentUserProvider)
        {
            _fairPlayDatingDbContext = fairPlayDatingDbContext;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<IQueryable<UserProfile>> GetMyMatchesAsync(CancellationToken cancellationToken)
        {
            var userObjectId = this._currentUserProvider.GetObjectId();
            var myUserProfile = await this._fairPlayDatingDbContext.UserProfile.AsNoTracking()
                .Include(p => p.ApplicationUser)
                .SingleOrDefaultAsync(p => p.ApplicationUser.AzureAdB2cobjectId.ToString() == userObjectId,
                cancellationToken: cancellationToken);
            if (myUserProfile is null)
                throw new Exception($"Unable to find user profile");
            var result = this._fairPlayDatingDbContext.UserProfile
                .Where(p => p.ApplicationUserId != myUserProfile.ApplicationUserId &&
                (
                    p.HairColorId == myUserProfile.PreferredHairColorId &&
                    p.BiologicalGenderId != myUserProfile.BiologicalGenderId &&
                    p.CurrentDateObjectiveId == myUserProfile.CurrentDateObjectiveId &&
                    p.EyesColorId == myUserProfile.PreferredEyesColorId &&
                    p.KidStatusId == myUserProfile.PreferredKidStatusId &&
                    p.ReligionId == myUserProfile.PreferredReligionId && 
                    p.TattooStatusId == myUserProfile.PreferredTattooStatusId
                ));
            return result;
        }
    }
}
