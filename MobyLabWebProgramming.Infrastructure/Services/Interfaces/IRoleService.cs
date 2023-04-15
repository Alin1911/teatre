using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;


namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ServiceResponse<RoleDTO>> GetRole(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<PagedResponse<RoleDTO>>> GetRoles(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
        Task<ServiceResponse> AddRole(RoleAddDTO role, CancellationToken cancellationToken = default);
        Task<ServiceResponse> UpdateRole(RoleUpdateDTO role, CancellationToken cancellationToken = default);
        Task<ServiceResponse> DeleteRole(Guid id, CancellationToken cancellationToken = default);
    }
}
