using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace App.Services.RealTimeUpdater.Infrastructure.FakeWattageMonitor
{
    public interface IFakeWattageMonitorService
    {
        void FakeWattageMonitorInit();
    }

    public class FakeWattageMonitorService : IFakeWattageMonitorService
    {
        private string[] locations = { "here", "there" };

        private Random _random;

        private IPublishEndpoint _publishEndpoint;

        private IFakeWattageMonitorServiceHelper _fakeWattageMonitorServiceHelper;

        public FakeWattageMonitorService(IPublishEndpoint publishEndpoint, IFakeWattageMonitorServiceHelper fakeWattageMonitorServiceHelper)
        {
            _publishEndpoint = publishEndpoint;
            _random = new Random();
            _fakeWattageMonitorServiceHelper = fakeWattageMonitorServiceHelper;
        }

        public void FakeWattageMonitorInit()
        {
            if (_fakeWattageMonitorServiceHelper.Activated)
            {
                return;
            }
            _publishEndpoint.Publish(FakeWattageUpdated());
            _fakeWattageMonitorServiceHelper.FakeWattageMonitorInitHelper(
                () => _publishEndpoint.Publish(FakeWattageUpdated())
                );
        }

        private WattageUpdatedEvent FakeWattageUpdated()
        {
            return new WattageUpdatedEvent
            {
                Location = locations[_random.Next(locations.Length)],
                Wattage = _random.Next(10, 15)
            };
        }
    }

    public interface IFakeWattageMonitorServiceHelper
    {
        bool Activated { get; set; }

        Task FakeWattageMonitorInitHelper(Action action);
    }

    public class FakeWattageMonitorServiceHelper : IFakeWattageMonitorServiceHelper
    {
        private PeriodicTimer? _timer;

        public bool Activated { get; set; } = false;

        public FakeWattageMonitorServiceHelper()
        {
        }

        public async Task FakeWattageMonitorInitHelper(Action action)
        {
            if (_timer != null || Activated)
            {
                return;
            }
            Activated = true;
            _timer = new PeriodicTimer(TimeSpan.FromSeconds(30));

            while (await _timer.WaitForNextTickAsync())
            {
                action();
            }
        }
    }
}
