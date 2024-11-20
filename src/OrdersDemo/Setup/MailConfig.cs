namespace OrdersDemo.Setup;

public class MailConfig
{
    public const string CONFIG_NAME = "Mail";

    public string? SmtpUrl { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
