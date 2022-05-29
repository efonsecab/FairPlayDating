using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using FairPlayDating.Models.UserPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.ClientServices
{
    [ClientServiceOfEntity(entityName: Constants.EntityNames.UserPhoto, primaryKeyType: typeof(long))]
    public partial class UserPhotoClientService
    {
        public async Task<UserPhotoModel> UploadMyPhotoAsync(Stream photoStream)
        {
            var requestUrl = "api/UserPhoto/UploadMyPhoto";
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                //Load the file and set the file's Content-Type header
                var fileStreamContent = new StreamContent(photoStream);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                //Add the file
                multipartFormContent.Add(fileStreamContent, name: "photo", fileName: "photo.png");

                //Send it
                var authorzedHttpClient = this._httpClientService.CreateAuthorizedClient();
                var response = await authorzedHttpClient.PostAsync(requestUrl, multipartFormContent);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<UserPhotoModel>();
                return result!;
            }
        }
    }
}
