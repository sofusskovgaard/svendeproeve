using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.RealTimeUpdater.Common.models
{
    public class EmissionModel
    {
        public CO2MeasurementModel? CO2MeasurementModel { get; set; } = new CO2MeasurementModel();

        public string Location { get; set; }

        public double Wattage { get; set; }

        public string CO2Emission
        {
            get
            {
                return $"{Wattage * CO2MeasurementModel.CO2g_pr_kwh}";
            }
        }
    }
}
