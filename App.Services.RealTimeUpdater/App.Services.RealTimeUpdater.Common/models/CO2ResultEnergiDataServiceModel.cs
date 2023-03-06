using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.RealTimeUpdater.Common.models
{
    public class CO2ResultEnergiDataServiceModel
    {

        public int total { get; set; }
        public int limit { get; set; }
        public string dataset { get; set; }
        public Record[] records { get; set; }
        public class Record
        {
            public DateTime Minutes5UTC { get; set; }
            public DateTime Minutes5DK { get; set; }
            public string PriceArea { get; set; }
            public double CO2Emission { get; set; }
        }
    }
}
