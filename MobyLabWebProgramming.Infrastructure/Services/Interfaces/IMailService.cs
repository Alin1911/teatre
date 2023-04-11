using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service can be used to send mails.
/// </summary>
public interface IMailService
{
    /// <summary>
    /// This method sends a mail using a recipient email, subject and body for the message that can be HTML by isHtmlBody true and it can also add a alias for your sender.
    /// </summary>
    public Task<ServiceResponse> SendMail(string recipientEmail, string subject, string body, bool isHtmlBody = false,
        string? senderTitle = default, CancellationToken cancellationToken = default);
}
