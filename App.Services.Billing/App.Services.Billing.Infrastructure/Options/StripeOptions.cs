namespace App.Services.Billing.Infrastructure.Options;

public static class StripeOptions
{
    public static string ApiKey => Environment.GetEnvironmentVariable("STRIPE_APIKEY") ?? "sk_test_51MgUIUIEBHRvaa49RhsnFSDeiLp0ZuRdu7GSsEynqHNxKot7lGttMNB2SGE5ws4uniQTkbuEuW0CiWELjpl7cwtF00GZ6Vn7PN";
}