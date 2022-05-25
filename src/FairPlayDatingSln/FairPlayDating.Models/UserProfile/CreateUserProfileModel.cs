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
        [Required]
        [StringLength(100)]
        public string? About { get; set; }
        [Required]
        public short? HairColorId { get; set; }
        [Required]
        public short? PreferredHairColorId { get; set; }
        [Required]
        public short? EyesColorId { get; set; }
        [Required]
        public short? PreferredEyesColorId { get; set; }
        [Required]
        public short? BiologicalGenderId { get; set; }
        [Required]
        public short? CurrentDateObjectiveId { get; set; }
        [Required]
        public short? ReligionId { get; set; }
        [Required]
        public short? PreferredReligionId { get; set; }
        [Required]
        public double? CurrentLatitude { get; set; }
        [Required]
        public double? CurrentLongitude { get; set; }
        [Required]
        public long? ProfileUserPhotoId { get; set; }
        [Required]
        public short? KidStatusId { get; set; }
        [Required]
        public short? PreferredKidStatusId { get; set; }
        [Required]
        public short? TattooStatusId { get; set; }
        [Required]
        public short? PreferredTattooStatusId { get; set; }
    }
}
