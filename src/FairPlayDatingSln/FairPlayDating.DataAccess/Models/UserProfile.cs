﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

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

        [ForeignKey(nameof(ApplicationUserId))]
        [InverseProperty("UserProfile")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey(nameof(BiologicalGenderId))]
        [InverseProperty(nameof(Gender.UserProfile))]
        public virtual Gender BiologicalGender { get; set; }
        [ForeignKey(nameof(CurrentDateObjectiveId))]
        [InverseProperty(nameof(DateObjective.UserProfile))]
        public virtual DateObjective CurrentDateObjective { get; set; }
        [ForeignKey(nameof(EyesColorId))]
        [InverseProperty("UserProfile")]
        public virtual EyesColor EyesColor { get; set; }
        [ForeignKey(nameof(HairColorId))]
        [InverseProperty("UserProfile")]
        public virtual HairColor HairColor { get; set; }
        [ForeignKey(nameof(ProfileUserPhotoId))]
        [InverseProperty(nameof(UserPhoto.UserProfile))]
        public virtual UserPhoto ProfileUserPhoto { get; set; }
        [ForeignKey(nameof(ReligionId))]
        [InverseProperty("UserProfile")]
        public virtual Religion Religion { get; set; }
    }
}