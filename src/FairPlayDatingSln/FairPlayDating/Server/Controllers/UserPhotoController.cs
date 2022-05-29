using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.CustomExceptions;
using FairPlayDating.Common.Global;
using FairPlayDating.Common.Interfaces;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.UserPhoto;
using FairPlayDating.Services;
using FairPlayDating.Services.Configuration;
using FairPlayDating.Services.Microservices.ComputerVision;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTI.Microservices.Library.Services;
using System;
using System.IO;
using System.Linq;
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
            [FromServices] ComputerVisionMicroservice computerVisionMicroservice,
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
            var imageAnalysisResult = await computerVisionMicroservice.AnalyzeFromImageUrlAsync(photoUri, cancellationToken: cancellationToken);
            if (imageAnalysisResult.faces?.Length != 1)
            {
                throw new CustomValidationException("We have been unanble to identify your face. " +
                    "Please use another photo, and make sure it only has one person.");
            }
            if (imageAnalysisResult.adult?.isAdultContent == true)
            {
                throw new CustomValidationException("Photo contains adult content");
            }
            if (imageAnalysisResult.adult?.isRacyContent == true)
            {
                throw new CustomValidationException("Photo contains racy content");
            }
            if (imageAnalysisResult.adult?.isGoryContent == true)
            {
                throw new CustomValidationException("Photo contains gory content");
            }
            var resultEntity = await userPhotoService.UploadMyPhotoAsync(photoUri, cancellationToken: cancellationToken);
            var resultModel = this.mapper.Map<UserPhoto, UserPhotoModel>(resultEntity);
            return resultModel;
        }

        [HttpGet("[action]")]
        public async Task<UserPhotoModel[]> GetUserPhotosByUserIdAsync(long applicationUserId, 
            CancellationToken cancellationToken)
        {
            var query = this.UserPhotoService.GetAllUserPhoto(
                trackEntities: false, cancellationToken: cancellationToken)
                .Where(p => p.ApplicationUserId == applicationUserId);
            var resultModel = await query
                .Select(p => mapper.Map<UserPhoto, UserPhotoModel>(p))
                .ToArrayAsync(cancellationToken: cancellationToken);
            return resultModel;
        }
    }
}
