using App.Services.RealTimeUpdater.Common.models;
using App.Services.RealTimeUpdater.Infrastructure.Cache;
using MassTransit;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.RealTimeUpdater.Infrastructure
{
    public class CO2apiService : ICO2apiService
    {
        public List<CO2MeasurementModel> Measurements { get; set; }
        private DataCache _memoryCache { get; set; }

        private readonly HttpClient _energiDataServiceClient;

        private TimeSpan expiration = TimeSpan.FromMinutes(15);

        public CO2apiService(DataCache memoryCache, HttpClient energiDataServiceClient)
        {
            _memoryCache = memoryCache;
            _energiDataServiceClient = energiDataServiceClient;
            Measurements = new List<CO2MeasurementModel>();
        }

        public async Task<CO2MeasurementModel?> GetMeasurementData(string country)
        {
            PurgeExpiredData();
            var data = Measurements.FirstOrDefault(measurement => measurement.Country == country);
            if (data != null)
            {
                return data!;
            }
            var newData = country switch
            {
                "denmark" => await GetDanishMeasurement(),
                "germany" => await GetGermanMeasurement(),
                _ => null,
            };
            Measurements.Add(newData);
            return newData;
        }

        private async Task<CO2MeasurementModel> GetDanishMeasurement()
        {
            CO2ResultEnergiDataServiceModel result;
            lock (lockobject.locker)
            {
                if (!_memoryCache.cache.TryGetValue("energy", out result))
                {
                    var request = new HttpRequestMessage
                    {
                        RequestUri = new Uri("https://api.energidataservice.dk/dataset/CO2Emis?limit=1")
                    };
                    var response = _energiDataServiceClient.Send(request);
                    result = response.Content.ReadFromJsonAsync<CO2ResultEnergiDataServiceModel>().Result;
                    _memoryCache.cache.Set("energy", result);
                }
            }

            //var result = await _redisCache.GetOrSetAsync<CO2ResultEnergiDataServiceModel>("energi", "data", async () =>
            //{
            //    var request = new HttpRequestMessage
            //    {
            //        RequestUri = new Uri("https://api.energidataservice.dk/dataset/CO2Emis?limit=1")
            //    };

            //    var response = await _energiDataServiceClient.SendAsync(request);
            //    return await response.Content.ReadFromJsonAsync<CO2ResultEnergiDataServiceModel>();
            //}, TimeSpan.FromMinutes(15));

            var model = new CO2MeasurementModel
            {
                CO2g_pr_kwh = result.records[0].CO2Emission,
                Country = "denmark",
                TimeStamp = DateTime.Now,
            };
            Measurements.Add(model);
            return model;
        }

        private async Task<CO2MeasurementModel> GetGermanMeasurement()
        {
            throw new NotImplementedException();
        }

        private int PurgeExpiredData()
        {
            return Measurements.RemoveAll(measurement => measurement.TimeStamp < DateTime.Now.Add(-expiration));
        }
    }
    public static class lockobject 
    {
        public static object locker = new object();
    }
}
