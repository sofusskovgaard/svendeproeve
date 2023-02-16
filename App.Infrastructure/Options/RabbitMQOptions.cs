namespace App.Infrastructure.Options;

public class RabbitMQOptions
{
    public string Host => Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "rabbitmq_master";

    public string Username => Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "user";

    public string Password => Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "bitnami";
}