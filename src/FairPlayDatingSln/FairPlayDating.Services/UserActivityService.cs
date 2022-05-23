using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using FairPlayDating.Common.Interfaces;
using FairPlayDating.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Services
{
    [ServiceOfEntity(entityName:Constants.EntityNames.UserActivity,primaryKeyType:typeof(long))]
    public partial class UserActivityService
    {
        public IQueryable<UserActivity> GetMyUserActivities()
        {
            var userObjectId = this._currentUserProvider.GetObjectId();
            var query = this._fairPlayDatingDbContext.UserActivity
                .Include(p=>p.ApplicationUser)
                .Where(p => p.ApplicationUser.AzureAdB2cobjectId.ToString() == userObjectId)
                .AsSplitQuery();
            return query;
        }

        public async Task UpdateMyUserActivityAsync(UserActivity userActivity, 
            CancellationToken cancellationToken)
        {
            var userObjectId = this._currentUserProvider.GetObjectId();
            var applicationUser = await this._fairPlayDatingDbContext.ApplicationUser
                .SingleAsync(p=>p.AzureAdB2cobjectId.ToString() == userObjectId,cancellationToken: cancellationToken);
            var existentUserActivity = 
                await this._fairPlayDatingDbContext.UserActivity.AsNoTracking()
                .SingleOrDefaultAsync(p => p.ActivityId == userActivity.ActivityId &&
                p.ApplicationUserId == applicationUser.ApplicationUserId, cancellationToken: cancellationToken);
            if (existentUserActivity != null)
                userActivity.UserActivityId = existentUserActivity.UserActivityId;
            userActivity.ApplicationUserId = applicationUser.ApplicationUserId;
            this._fairPlayDatingDbContext.UserActivity.Update(userActivity);
            await this._fairPlayDatingDbContext.SaveChangesAsync(cancellationToken:cancellationToken);
        }
    }
}
