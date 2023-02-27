namespace App.Infrastructure.Options;

public static class JwtOptions
{
    public static string Issuer => Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "app";
    public static string Audience => Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "app";
    public static string Key => Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "this_secret_has_to_be_at_least_128_bits_long";
    public static uint TokenLifeTime => uint.TryParse(Environment.GetEnvironmentVariable("JWT_TOKEN_LIFE_TIME"), out var tokenLifeTime) ? tokenLifeTime : 1800;
}