using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.RealTimeUpdater.Common.models
{
    public class CO2MeasurementModel
    {
        public double CO2g_pr_kwh { get; set; } = 0;

        public string Country { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
