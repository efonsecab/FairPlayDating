﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FairPlayDating.DataAccess.Models
{
    public partial class UserEyesColorPreference
    {
        [Key]
        public long UserEyesColorPreferenceId { get; set; }
        public long ApplicationUserId { get; set; }
        public short EyesColorId { get; set; }

        [ForeignKey("ApplicationUserId")]
        [InverseProperty("UserEyesColorPreference")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("EyesColorId")]
        [InverseProperty("UserEyesColorPreference")]
        public virtual EyesColor EyesColor { get; set; }
    }
}