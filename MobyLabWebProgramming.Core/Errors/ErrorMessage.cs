using System.Net;
using System.Text.Json.Serialization;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// This is a simple class to transmit the error information to the client.
/// It includes the message, custom error code to identify te specific error and the HTTP status code to be set on the HTTP response.
/// </summary>
public class ErrorMessage
{
    public string Message { get; }
    public ErrorCodes Code { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HttpStatusCode Status { get; }

    public ErrorMessage(HttpStatusCode status, string message, ErrorCodes code = ErrorCodes.Unknown)
    {
        Message = message;
        Status = status;
        Code = code;
    }

    public static ErrorMessage FromException(ServerException exception) => new(exception.Status, exception.Message);
}
