namespace Infrastructure.Mailer;

public interface IMailService
{
    Task SendAsync(IMailMessage message, CancellationToken cancellationToken);
}