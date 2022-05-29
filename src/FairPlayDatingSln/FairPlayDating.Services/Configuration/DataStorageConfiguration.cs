using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Services.Configuration
{
    public class DataStorageConfiguration
    {
        [Required]
        public string? ContainerName { get; set; }
        [Required]
        public string? AccountName { get; set; }
    }
}
