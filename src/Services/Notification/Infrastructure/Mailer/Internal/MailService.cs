namespace Infrastructure.Mailer.Internal;

public class MailService : IMailService
{
    public Task SendAsync(IMailMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}