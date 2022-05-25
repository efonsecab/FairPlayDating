using Blazored.Toast.Services;
using FairPlayDating.ClientServices;
using FairPlayDating.Models.Facebook.GetMyAlbums;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.MauiBlazor.Components.Facebook
{
    public partial class MyAlbums
    {
        public GetMyAlbumsResponse MyAlbumsPage { get; private set; }
        [Inject]
        private FacebookClientService FacebookClientService { get; set; }
        [Inject]
        private IToastService ToastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.MyAlbumsPage = await this.FacebookClientService.GetMyAlbumsAsync();
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
        }

        private async Task OnAlbumImageClicked(Datum album)
        {
            await Task.Yield();
        }
    }
}
