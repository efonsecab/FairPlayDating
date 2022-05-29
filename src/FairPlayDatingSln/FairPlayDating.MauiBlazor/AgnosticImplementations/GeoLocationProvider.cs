using FairPlayDating.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.MauiBlazor.AgnosticImplementations
{
    internal class GeoLocationProvider : IGeoLocationProvider
    {
        public async Task<GeoCoordinates> GetCurrentPositionAsync()
        {
            var locationTaskCompletionSource = new TaskCompletionSource<Location>();

            Application.Current.Dispatcher.Dispatch(async () => 
            {
                locationTaskCompletionSource.SetResult(await Geolocation.GetLocationAsync());
            });

            await locationTaskCompletionSource.Task.ConfigureAwait(false);
            var geoCoorindates = new GeoCoordinates
            {
                Latitude = locationTaskCompletionSource.Task.Result.Latitude,
                Longitude = locationTaskCompletionSource.Task.Result.Longitude
            };
            return geoCoorindates;
        }
    }
}
