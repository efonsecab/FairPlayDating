using FairPlayDating.Models.UserActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.MauiBlazor.ExtendedModels
{
    public class ExtendedUserActivityModel: UserActivityModel
    {
        public string ActivityName { get; set; }
        public string FrequencyName { get; set; }
    }
}
