using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Models.UserProfile
{
    public class CreateUserProfileModel
    {
        public long ApplicationUserId { get; set; }
        [Required]
        [StringLength(100)]
        public string About { get; set; }
        public short HairColorId { get; set; }
        public short EyesColorId { get; set; }
        public short BiologicalGenderId { get; set; }
        public short CurrentDateObjectiveId { get; set; }
        public short ReligionId { get; set; }
        public double CurrentLatitude { get; set; }
        public double CurrentLongitude { get; set; }
        public long ProfileUserPhotoId { get; set; }
        public short KidStatusId { get; set; }
        public short PreferredKidStatusId { get; set; }
        public short TattooStatusId { get; set; }
        public short PreferredTattooStatusId { get; set; }
    }
}
