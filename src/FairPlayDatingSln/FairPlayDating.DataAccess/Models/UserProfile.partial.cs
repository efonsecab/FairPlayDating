using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.DataAccess.Models
{
    public partial class UserProfile
    {
        //Check https://docs.microsoft.com/en-us/ef/core/modeling/spatial
        [Required]
        public Point CurrentGeoLocation { get; set; }
    }
}
