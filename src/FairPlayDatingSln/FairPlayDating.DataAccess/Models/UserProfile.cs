﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FairPlayDating.DataAccess.Models
{
    public partial class UserProfile
    {
        [Key]
        public long UserProfileId { get; set; }
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

        [ForeignKey("ApplicationUserId")]
        [InverseProperty("UserProfile")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("BiologicalGenderId")]
        [InverseProperty("UserProfile")]
        public virtual Gender BiologicalGender { get; set; }
        [ForeignKey("CurrentDateObjectiveId")]
        [InverseProperty("UserProfile")]
        public virtual DateObjective CurrentDateObjective { get; set; }
        [ForeignKey("EyesColorId")]
        [InverseProperty("UserProfile")]
        public virtual EyesColor EyesColor { get; set; }
        [ForeignKey("HairColorId")]
        [InverseProperty("UserProfile")]
        public virtual HairColor HairColor { get; set; }
        [ForeignKey("KidStatusId")]
        [InverseProperty("UserProfileKidStatus")]
        public virtual KidStatus KidStatus { get; set; }
        [ForeignKey("PreferredKidStatusId")]
        [InverseProperty("UserProfilePreferredKidStatus")]
        public virtual KidStatus PreferredKidStatus { get; set; }
        [ForeignKey("PreferredTattooStatusId")]
        [InverseProperty("UserProfilePreferredTattooStatus")]
        public virtual TattooStatus PreferredTattooStatus { get; set; }
        [ForeignKey("ProfileUserPhotoId")]
        [InverseProperty("UserProfile")]
        public virtual UserPhoto ProfileUserPhoto { get; set; }
        [ForeignKey("ReligionId")]
        [InverseProperty("UserProfile")]
        public virtual Religion Religion { get; set; }
        [ForeignKey("TattooStatusId")]
        [InverseProperty("UserProfileTattooStatus")]
        public virtual TattooStatus TattooStatus { get; set; }
    }
}