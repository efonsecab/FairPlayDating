using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Models.Activity
{
    public class CreateActivityModel
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
    }
}
