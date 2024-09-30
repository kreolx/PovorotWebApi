namespace PL.Contracts.Settings;

public class RabbitSettings
{
    public const int DefaultNumberOfRetries = 20;
    public const int DefaultRetryIncrement = 60;
    public string Uri { get; set; } = default!;

    public string VirtualHost { get; set; } = default!;

    public ushort Port { get; init; } = 5672;

    public string Username { get; set; } = default!;

    public string Password { get; set; } = default!;

    public int Retries { get; set; } = DefaultNumberOfRetries;

    public int RetryIncrement { get; set; } = DefaultRetryIncrement;
}