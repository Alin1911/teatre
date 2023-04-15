using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Requests;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IActorService
{
    Task<ServiceResponse<ActorDTO>> GetActor(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceResponse<PagedResponse<ActorDTO>>> GetActors(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    Task<ServiceResponse> AddActor(ActorAddDTO actor, CancellationToken cancellationToken = default);
    Task<ServiceResponse> UpdateActor(ActorUpdateDTO actor, CancellationToken cancellationToken = default);
    Task<ServiceResponse> DeleteActor(Guid id, CancellationToken cancellationToken = default);
}
