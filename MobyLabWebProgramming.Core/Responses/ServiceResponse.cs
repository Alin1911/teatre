using MobyLabWebProgramming.Core.Errors;

namespace MobyLabWebProgramming.Core.Responses;

/// <summary>
/// These classes are used as responses from service methods as either a success responses or as error responses.
/// </summary>
public class ServiceResponse
{
    public ErrorMessage? Error { get; set; }
    public bool IsOk => Error == null;

    public static ServiceResponse FromError(ErrorMessage? error) => new() { Error = error };
    public static ServiceResponse ForSuccess() => new();

    protected ServiceResponse() { }
}

public class ServiceResponse<T> : ServiceResponse
{
    public T? Result { get; set; }

    public new static ServiceResponse<T> FromError(ErrorMessage? error) => new() { Error = error };
    public static ServiceResponse<T> ForSuccess(T data) => new() { Result = data };
    public ServiceResponse ToResponse() => ServiceResponse.FromError(Error);

    private ServiceResponse() { }
}

/// <summary>
/// These are extension methods for the ServiceResponse classes.
/// They can be used to functionally process data within the ServiceResponse object.
/// </summary>
public static class ServiceResponseExtension
{
    public static ServiceResponse<TOut> Map<TIn, TOut>(this ServiceResponse<TIn> response, Func<TIn, TOut> selector) where TIn : class where TOut : class =>
        response.Result != null ? ServiceResponse<TOut>.ForSuccess(selector(response.Result)) : ServiceResponse<TOut>.FromError(response.Error);

    public static ServiceResponse<TOut> FlatMap<TIn, TOut>(this ServiceResponse<TIn> response, Func<TIn, ServiceResponse<TOut>> selector) where TIn : class where TOut : class =>
        response.Result != null ? selector(response.Result) : ServiceResponse<TOut>.FromError(response.Error);

    public static ServiceResponse<TOut> FlatMap<TOut>(this ServiceResponse response, Func<ServiceResponse<TOut>> selector) where TOut : class =>
        response.Error == null ? selector() : ServiceResponse<TOut>.FromError(response.Error);

    public static ServiceResponse<TIn> Flatten<TIn>(this ServiceResponse<ServiceResponse<TIn>> response) where TIn : class =>
        response.Result ?? ServiceResponse<TIn>.FromError(response.Error);

    public static ServiceResponse Flatten(this ServiceResponse<ServiceResponse> response) =>
        response.Result ?? ServiceResponse.FromError(response.Error);
}
