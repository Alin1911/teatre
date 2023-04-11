using System.Text.Json.Serialization;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// This enumeration represents codes for common errors and should be used to better identify the error by the client. You may add or remove codes as you see fit.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorCodes
{
    Unknown,
    TechnicalError,
    EntityNotFound,
    PhysicalFileNotFound,
    UserAlreadyExists,
    WrongPassword,
    CannotAdd,
    CannotUpdate,
    CannotDelete,
    MailSendFailed
}
