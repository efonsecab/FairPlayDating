﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FairPlayDating.DataAccess.Models
{
    public partial class UserPhoto
    {
        public UserPhoto()
        {
            UserProfile = new HashSet<UserProfile>();
        }

        [Key]
        public long UserPhotoId { get; set; }
        public long ApplicationUserId { get; set; }
        [Required]
        [StringLength(500)]
        public string PhotoBlobUrl { get; set; }

        [ForeignKey("ApplicationUserId")]
        [InverseProperty("UserPhoto")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [InverseProperty("ProfileUserPhoto")]
        public virtual ICollection<UserProfile> UserProfile { get; set; }
    }
}