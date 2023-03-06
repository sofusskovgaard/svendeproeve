using App.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.RealTimeUpdater.Infrastructure.FakeWattageMonitor
{
    public class WattageUpdatedEvent : IEventMessage
    {
        public double Wattage { get; set; }

        public string Location { get; set; }
    }
}
