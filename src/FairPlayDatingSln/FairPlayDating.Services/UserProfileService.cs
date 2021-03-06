using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using FairPlayDating.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Services
{
    [ServiceOfEntity(entityName: Constants.EntityNames.UserProfile, primaryKeyType: typeof(long))]
    public partial class UserProfileService
    {
        public IQueryable<UserProfile> GetMyUserProfile()
        {
            var userObjectId = this._currentUserProvider.GetObjectId();
            var result = this._fairPlayDatingDbContext.UserProfile
                .Include(p=>p.ApplicationUser)
                .Where(p => p.ApplicationUser.AzureAdB2cobjectId.ToString() == userObjectId);
            return result;
        }

        public async Task UpdateMyUserProfileAsync(UserProfile userProfile, 
            CancellationToken cancellationToken)
        {

            var userObjectId = this._currentUserProvider.GetObjectId();
            var applicationUser = await this._fairPlayDatingDbContext.ApplicationUser
                .SingleAsync(p => p.AzureAdB2cobjectId.ToString() == userObjectId, cancellationToken: cancellationToken);
            var existentUserProfile =
                await this._fairPlayDatingDbContext.UserProfile.AsNoTracking()
                .SingleOrDefaultAsync(p => p.UserProfileId == userProfile.UserProfileId &&
                p.ApplicationUserId == applicationUser.ApplicationUserId, cancellationToken: cancellationToken);
            if (existentUserProfile != null)
            {
                userProfile.UserProfileId = existentUserProfile.UserProfileId;
            }
            this._fairPlayDatingDbContext.UserProfile.Update(userProfile);
            await this._fairPlayDatingDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
        }
    }
}
