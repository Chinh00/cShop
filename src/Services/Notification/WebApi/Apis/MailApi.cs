using System.Net.Mail;
using Asp.Versioning.Builder;

namespace WebApi.Apis;

public static class MailApi 
{
    public const string BaseUrl = "/api/v{version:apiVersion}/mail";
    public static IVersionedEndpointRouteBuilder MapMailApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);
        group.MapGet(string.Empty, async () =>
        {
            var smtp = new SmtpClient()
            {
                Port = 25,
                Host = "localhost",
                EnableSsl = false
            };
            await smtp.SendMailAsync(new MailMessage()
            {
                From = new MailAddress("noreply@gmail.com"),
                Subject = "asasd",
                Body = "Thank you!",
                To = { "noreply1111@gmail.com" }
            });
        });
        
        return endpoints;
    }
}