using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// This is an alternative solution to the ServiceResponse class if you rather want to use exceptions.
/// You may add more exceptions for other HTTP codes as you like.
/// </summary>
public class ServerException : Exception
{
    public HttpStatusCode Status { get; }
    public ErrorCodes Code { get; }
    public ServerException(HttpStatusCode status, string message, ErrorCodes code = ErrorCodes.Unknown) : base(message)
    {
        Status = status;
        Code = code;
    }
}

public class InternalServerErrorException : ServerException
{
    public InternalServerErrorException(string message = "Something went wrong!", ErrorCodes code = ErrorCodes.Unknown) : base(HttpStatusCode.InternalServerError, message, code)
    {
    }
}

public class ForbiddenException : ServerException
{
    public ForbiddenException(string message, ErrorCodes code = ErrorCodes.Unknown) : base(HttpStatusCode.Forbidden, message, code)
    {
    }
}

public class BadRequestException : ServerException
{
    public BadRequestException(string message, ErrorCodes code = ErrorCodes.Unknown) : base(HttpStatusCode.BadRequest, message, code)
    {
    }
}

public class UnauthorizedException : ServerException
{
    public UnauthorizedException(string message, ErrorCodes code = ErrorCodes.Unknown) : base(HttpStatusCode.Unauthorized, message, code)
    {
    }
}

public class NotFoundException : ServerException
{
    public NotFoundException(string message, ErrorCodes code = ErrorCodes.Unknown) : base(HttpStatusCode.NotFound, message, code)
    {
    }
}

public class ServiceUnavailableException : ServerException
{
    public ServiceUnavailableException(string message, ErrorCodes code = ErrorCodes.Unknown) : base(HttpStatusCode.ServiceUnavailable, message, code)
    {
    }
}

public class ConflictException : ServerException
{
    public ConflictException(string message, ErrorCodes code = ErrorCodes.Unknown) : base(HttpStatusCode.Conflict, message, code)
    {
    }
}
