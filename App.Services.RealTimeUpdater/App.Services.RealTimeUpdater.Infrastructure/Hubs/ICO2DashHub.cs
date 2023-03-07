namespace App.Services.RealTimeUpdater.Infrastructure.Hubs
{
    public interface ICO2DashHub
    {
        Task SendCO2Update(string location, double kwh);
    }
}