using App.Services.RealTimeUpdater.Common.models;
using App.Services.RealTimeUpdater.Infrastructure.FakeWattageMonitor;
using App.Services.Turnaments.Common.Dtos;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.RealTimeUpdater.Infrastructure.Hubs
{
    public class CO2DashHub : Hub, ICO2DashHub
    {

        private IHubContext<CO2DashHub> _context;

        private ICO2apiService _cO2ApiService;

        private IFakeWattageMonitorService _fakeWattageMonitorService;

        public CO2DashHub(IHubContext<CO2DashHub> context, ICO2apiService cO2ApiService, IFakeWattageMonitorService fakeWattageMonitorService)
        {
            _context = context;
            _cO2ApiService = cO2ApiService;
            _fakeWattageMonitorService = fakeWattageMonitorService;
            StartFaker();
        }

        public void StartFaker()
        {
            _fakeWattageMonitorService.FakeWattageMonitorInit();
        }

        public async Task SendCO2Update(string location, double kwh)
        {
            var measurement = await _cO2ApiService.GetMeasurementData(GetCountryByLocation(location));
            var emission = new EmissionModel 
            {
                CO2MeasurementModel = measurement,
                Wattage = kwh,
                Location = location,
            };

            await _context.Clients.All.SendAsync("location-" + location, emission);
        }
        private string GetCountryByLocation(string location)
        {
            return "denmark";
        }
    }
}
