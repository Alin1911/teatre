using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service will be uses to mange user information.
/// As most routes and business logic will need to know what user is currently using the backend this service will be the most used.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// GetUser will provide the information about a user given its user Id.
    /// </summary>
    public Task<ServiceResponse<UserDTO>> GetUser(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetUsers returns page with user information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<UserDTO>>> GetUsers(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    /// <summary>
    /// Login as suggested responds to a user login request with the JWT token and user information.
    /// </summary>
    public Task<ServiceResponse<LoginResponseDTO>> Login(LoginDTO login, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetUserCount returns the number of users in the database.
    /// </summary>
    public Task<ServiceResponse<int>> GetUserCount(CancellationToken cancellationToken = default);
    /// <summary>
    /// AddUser adds an user and verifies if requesting user has permissions to add one.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> AddUser(UserAddDTO user, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// UpdateUser updates an user and verifies if requesting user has permissions to update it, if the user is his own then that should be allowed.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateUser(UserUpdateDTO user, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// DeleteUser deletes an user and verifies if requesting user has permissions to delete it, if the user is his own then that should be allowed.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteUser(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
