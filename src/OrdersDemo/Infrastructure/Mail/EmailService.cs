using Microsoft.Extensions.Options;
using OrdersDemo.Setup;

namespace OrdersDemo.Infrastructure.Mail;

public class EmailService : IEmailService
{
    private readonly MailConfig _mailConfig;

    public EmailService(IOptions<MailConfig> options)
    {
        _mailConfig = options.Value;
    }

    public Task SendEmailAsync()
    {
        // Do nothing
        return Task.CompletedTask;
    }
}
