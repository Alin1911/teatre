using System.Net;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Extensions;

/// <summary>
/// This static class contains extension methods for controllers to set the response objects and status codes in a easier way.
/// </summary>
public static class ControllerExtensions
{
    /// <summary>
    /// Notice that the following methods adapt the responses or errors to a ActionResult with a status code that will be serialized into the HTTP response body.
    /// </summary>
    public static ActionResult<RequestResponse> ErrorMessageResult(this ControllerBase controller, ServerException serverException) =>
        controller.StatusCode((int)serverException.Status, RequestResponse.FromError(ErrorMessage.FromException(serverException))); // The StatusCode method of the controller base will
                                                                                                                                            // set the given HTTP status code in the response and will serialize
                                                                                                                                            // the response object.

    public static ActionResult<RequestResponse> ErrorMessageResult(this ControllerBase controller, ErrorMessage? errorMessage = default) =>
        controller.StatusCode((int)(errorMessage?.Status ?? HttpStatusCode.InternalServerError), RequestResponse.FromError(errorMessage));

    public static ActionResult<RequestResponse<T>> ErrorMessageResult<T>(this ControllerBase controller, ErrorMessage? errorMessage = default) =>
        controller.StatusCode((int)(errorMessage?.Status ?? HttpStatusCode.InternalServerError), RequestResponse<T>.FromError(errorMessage));

    public static ActionResult<RequestResponse> FromServiceResponse(this ControllerBase controller, ServiceResponse response) =>
        response.Error == null ? controller.Ok(RequestResponse.OkRequestResponse) : controller.ErrorMessageResult(response.Error); // The Ok method of the controller base will set the
                                                                                                                                      // HTTP status code in the response to 200 Ok and will
                                                                                                                                      // serialize the response object.

    public static ActionResult<RequestResponse<T>> FromServiceResponse<T>(this ControllerBase controller, ServiceResponse<T> response) =>
        response.Error == null ? controller.Ok(RequestResponse<T>.FromServiceResponse(response)) : controller.ErrorMessageResult<T>(response.Error);

    public static ActionResult<RequestResponse> OkRequestResponse(this ControllerBase controller) => controller.Ok(RequestResponse.OkRequestResponse);
}
