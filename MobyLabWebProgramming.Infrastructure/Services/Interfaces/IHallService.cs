using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    public interface IHallService
    {
        Task<ServiceResponse<HallDTO>> GetHall(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<PagedResponse<HallDTO>>> GetHalls(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
        Task<ServiceResponse> AddHall(HallAddDTO hall, CancellationToken cancellationToken = default);
        Task<ServiceResponse> UpdateHall(HallUpdateDTO hall, CancellationToken cancellationToken = default);
        Task<ServiceResponse> DeleteHall(Guid id, CancellationToken cancellationToken = default);
    }
}
