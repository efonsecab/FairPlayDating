using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Common.Interfaces
{
    public interface IGeoLocationProvider
    {
        Task<GeoCoordinates> GetCurrentPositionAsync();
    }
}
