using FairPlayDating.ClientServices;
using FairPlayDating.Common.Global;
using FairPlayDating.Models.Facebook.GetMyAlbums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace FairPlayDating.Client.Pages.Users
{
    [Authorize]
    [Route(Constants.ClientRoutes.MyAlbums)]
    public partial class MyAlbums
    {
        [Inject]
        private FacebookClientService FacebookClientService { get; set; }
        private string PageToken { get; set; } = null;
        private GetMyAlbumsResponse PageAlbums{ get; set; }
        protected async override Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            this.PageAlbums= await this.FacebookClientService.GetMyAlbums(PageToken);
        }

        private async Task OnNextPageClick()
        {
            this.PageToken = this.PageAlbums.paging.cursors.after;
            await LoadData();
        }
    }
}
