using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public RoleService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }
        public Task<ServiceResponse> AddRole(RoleAddDTO role, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteRole(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<RoleDTO>> GetRole(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetAsync(new RoleProjectionSpec(id), cancellationToken);

            return result != null ?
                ServiceResponse<RoleDTO>.ForSuccess(result) :
                ServiceResponse<RoleDTO>.FromError(CommonErrors.RoleNotFound);
        }

        public async Task<ServiceResponse<PagedResponse<RoleDTO>>> GetRoles(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
        {
            var result = await _repository.PageAsync(pagination, new RoleProjectionSpec(pagination.Search), cancellationToken);

            return ServiceResponse<PagedResponse<RoleDTO>>.ForSuccess(result);
        }

        public Task<ServiceResponse> UpdateRole(RoleUpdateDTO hall, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
