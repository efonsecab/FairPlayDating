using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Models.Gender
{
    public class CreateGenderModel
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
