using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Models.KidStatus
{
    public class CreateKidStatusModel
    {
        public short KidStatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
