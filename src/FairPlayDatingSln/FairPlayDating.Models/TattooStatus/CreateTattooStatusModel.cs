using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Models.TattooStatus
{
    public class CreateTattooStatusModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
