namespace App.Web.Config;

public static class ApiOptions
{
    public static string Host => Environment.GetEnvironmentVariable("API_HOST") ?? "https://localhost:3000";
}