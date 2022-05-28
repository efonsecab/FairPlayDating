using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Models.UserPhoto
{
    public class CreateUserPhotoModel
    {
        [Required]
        [StringLength(500)]
        public string? PhotoBlobUrl { get; set; }
    }
}
