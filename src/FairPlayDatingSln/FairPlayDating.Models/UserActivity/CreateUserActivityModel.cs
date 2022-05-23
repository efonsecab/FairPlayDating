using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Models.UserActivity
{
    public class CreateUserActivityModel
    {
        public long UserActivityId { get; set; }
        public long ApplicationUserId { get; set; }
        public short ActivityId { get; set; }
        public short FrequencyId { get; set; }
    }
}
