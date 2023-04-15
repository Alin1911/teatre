using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage HallNotFound => new(HttpStatusCode.NotFound, "Hall doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage RoleNotFound => new(HttpStatusCode.NotFound, "Role doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ActorNotFound => new(HttpStatusCode.NotFound, "Actor doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage TicketNotFound => new(HttpStatusCode.NotFound, "Ticket doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage TransactionNotFound => new(HttpStatusCode.NotFound, "Transaction doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage PieceNotFound => new(HttpStatusCode.NotFound, "Piece not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage PerformanceNotFound => new(HttpStatusCode.NotFound, "Performance doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
}
