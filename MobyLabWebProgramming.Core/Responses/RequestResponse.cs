using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Errors;

namespace MobyLabWebProgramming.Core.Responses;

/// <summary>
/// This class is used to encapsulate data or errors for the client as a response to the HTTP request.
/// </summary>
public class RequestResponse<T>
{
    /// <summary>
    /// This is the response to the request, if an error occurred this should be null. 
    /// </summary>
    public T? Response { get; private init; }
    /// <summary>
    /// This is the error message for the error that occurred while responding to the request, if no error occurred this should be null. 
    /// </summary>
    public ErrorMessage? ErrorMessage { get; private init; }
    public ServiceResponse<PagedResponse<ActorDTO>> Actors { get; }

    protected RequestResponse() { }

    public RequestResponse(ServiceResponse<PagedResponse<ActorDTO>> actors)
    {
        Actors = actors;
    }

    public static RequestResponse FromError(ErrorMessage? error)
    {
        return error != null
            ? new RequestResponse
            {
                ErrorMessage = error
            }
            : new()
            {
                Response = "Ok"
            };
    }

    public static RequestResponse<string> FromServiceResponse(ServiceResponse serviceResponse)
    {
        return FromError(serviceResponse.Error);
    }

    public static RequestResponse<T> FromServiceResponse(ServiceResponse<T> serviceResponse)
    {
        return serviceResponse.Error != null
            ? new RequestResponse<T>
            {
                ErrorMessage = serviceResponse.Error
            }
            : new()
            {
                Response = serviceResponse.Result
            };
    }
}

public class RequestResponse : RequestResponse<string>
{
    public static RequestResponse OkRequestResponse => FromError(null);
}
