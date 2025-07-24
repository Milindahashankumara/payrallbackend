using payrallproject.Models.Helpter;

namespace payrallproject.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(Mailrequest mailrequest);
    }
}
