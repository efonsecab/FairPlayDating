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

        protected override async Task OnInitializedAsync()
        {
            this.MyAlbumsPage = await this.FacebookClientService.GetMyAlbums();
        }
    }
}
