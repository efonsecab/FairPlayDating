using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using FairPlayDating.Common.Interfaces;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.UserPhoto;
using FairPlayDating.Services;
using FairPlayDating.Services.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTI.Microservices.Library.Services;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FairPlayDating.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ControllerOfEntity(entityName: Constants.EntityNames.UserPhoto, primaryKeyType: typeof(long))]
    public partial class UserPhotoController : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<UserPhotoModel> UploadMyPhotoAsync(IFormFile photo,
            [FromServices] ICurrentUserProvider currentUserProvider,
            [FromServices] AzureBlobStorageService azureBlobStorageService,
            [FromServices] DataStorageConfiguration dataStorageConfiguration,
            [FromServices] UserPhotoService userPhotoService,
            CancellationToken cancellationToken)
        {
            var userObjectId = currentUserProvider.GetObjectId();
            var photoStream = photo.OpenReadStream();
            var fileExtension = Path.GetExtension(photo.FileName);
            var storeFileName = $"{Path.GetRandomFileName()}{fileExtension}";
            var uploadFileResult = await azureBlobStorageService.UploadFileAsync(
                dataStorageConfiguration.ContainerName, $"{userObjectId}/{storeFileName}",
                photoStream, overwrite: false, cancellationToken);
            Uri photoUri = 
                new Uri($"https://{dataStorageConfiguration.AccountName}.blob.core.windows.net/" +
                $"{dataStorageConfiguration.ContainerName}/{userObjectId}/{storeFileName}");
            var resultEntity = await userPhotoService.UploadMyPhotoAsync(photoUri, cancellationToken: cancellationToken);
            var resultModel = this.mapper.Map<UserPhoto, UserPhotoModel>(resultEntity);
            return resultModel;
        }
    }
}
