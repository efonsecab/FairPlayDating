using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Models.Religion
{
    public class CreateReligionModel
    {
        [Required]
        [StringLength(20)]
        public string? Name { get; set; }
    }
}
