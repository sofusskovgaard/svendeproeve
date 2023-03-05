using App.Services.RealTimeUpdater.Common.models;

namespace App.Services.RealTimeUpdater.Infrastructure
{
    public interface ICO2apiService
    {
        List<CO2MeasurementModel> Measurements { get; set; }

        Task<CO2MeasurementModel?> GetMeasurementData(string country);
    }
}