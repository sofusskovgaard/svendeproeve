namespace App.Infrastructure.Options;

public static class JwtOptions
{
    public static string Issuer => Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "app";
    public static string Audience => Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "app";
    public static string Key => Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "secret";
}