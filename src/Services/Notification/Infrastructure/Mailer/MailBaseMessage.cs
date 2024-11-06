using System.Net.Mail;
using System.Text;

namespace Infrastructure.Mailer;

public class MailBaseMessage : MailAddress, IMailMessage
{
    public MailBaseMessage(string address) : base(address)
    {
    }

    public MailBaseMessage(string address, string displayName) : base(address, displayName)
    {
    }

    public MailBaseMessage(string address, string displayName, Encoding displayNameEncoding) : base(address, displayName, displayNameEncoding)
    {
    }
}