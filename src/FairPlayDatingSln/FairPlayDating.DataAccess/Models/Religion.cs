﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FairPlayDating.DataAccess.Models
{
    public partial class Religion
    {
        public Religion()
        {
            UserProfilePreferredReligion = new HashSet<UserProfile>();
            UserProfileReligion = new HashSet<UserProfile>();
        }

        [Key]
        public short ReligionId { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string Name { get; set; }

        [InverseProperty("PreferredReligion")]
        public virtual ICollection<UserProfile> UserProfilePreferredReligion { get; set; }
        [InverseProperty("Religion")]
        public virtual ICollection<UserProfile> UserProfileReligion { get; set; }
    }
}