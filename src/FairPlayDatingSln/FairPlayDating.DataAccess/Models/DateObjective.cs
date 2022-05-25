﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FairPlayDating.DataAccess.Models
{
    public partial class DateObjective
    {
        public DateObjective()
        {
            UserProfileCurrentDateObjective = new HashSet<UserProfile>();
            UserProfilePreferredCurrentDateObjective = new HashSet<UserProfile>();
        }

        [Key]
        public short DateObjectiveId { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string Name { get; set; }

        [InverseProperty("CurrentDateObjective")]
        public virtual ICollection<UserProfile> UserProfileCurrentDateObjective { get; set; }
        [InverseProperty("PreferredCurrentDateObjective")]
        public virtual ICollection<UserProfile> UserProfilePreferredCurrentDateObjective { get; set; }
    }
}