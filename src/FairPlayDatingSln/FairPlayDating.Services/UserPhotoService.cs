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
    [ServiceOfEntity(entityName: Constants.EntityNames.UserPhoto, primaryKeyType: typeof(long))]
    public partial class UserPhotoService
    {
        public async Task<UserPhoto> UploadMyPhotoAsync(Uri photoUrl, CancellationToken cancellationToken)
        {
            var userObjectId = this._currentUserProvider.GetObjectId();
            var userEntity = await this._fairPlayDatingDbContext.ApplicationUser
                .SingleOrDefaultAsync(p => p.AzureAdB2cobjectId.ToString() == userObjectId, cancellationToken);
            if (userEntity is null)
            {
                throw new Exception("User not found");
            }
            UserPhoto userPhoto = new UserPhoto()
            {
                ApplicationUserId = userEntity.ApplicationUserId,
                PhotoBlobUrl = photoUrl.ToString()
            };
            await this._fairPlayDatingDbContext.UserPhoto.AddAsync(userPhoto, cancellationToken: cancellationToken);
            await this._fairPlayDatingDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
            return userPhoto;
        }
    }
}
