using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Models.UserPhoto
{
    public class UserPhotoModel
    {
        public long UserPhotoId { get; set; }
        public long ApplicationUserId { get; set; }
        public string? PhotoBlobUrl { get; set; }
    }
}
