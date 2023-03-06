using App.Infrastructure.Events;
using App.Services.RealTimeUpdater.Infrastructure.FakeWattageMonitor;
using App.Services.RealTimeUpdater.Infrastructure.Hubs;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.RealTimeUpdater.Infrastructure.EventHandlers
{
    public class WattageUpdatedEventHandler : IEventHandler<WattageUpdatedEvent>
    {
        private ICO2DashHub _co2Hub;

        public WattageUpdatedEventHandler(ICO2DashHub co2Hub)
        {
            _co2Hub = co2Hub;
        }

        public async Task Consume(ConsumeContext<WattageUpdatedEvent> context)
        {
            var message = context.Message;

            await _co2Hub.SendCO2Update(message.Location, message.Wattage);
        }
    }
}
