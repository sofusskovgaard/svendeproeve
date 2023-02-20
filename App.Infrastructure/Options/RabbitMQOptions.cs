namespace App.Infrastructure.Options;

public class RabbitMQOptions
{
    public string Host => Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "rabbitmq";

    public string Username => Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "rabbitmq";

    public string Password => Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "rabbitmq";
}