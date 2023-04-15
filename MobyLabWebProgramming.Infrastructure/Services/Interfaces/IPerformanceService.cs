using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;


namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    public interface IPerformanceService
    {
        Task<ServiceResponse<PerformanceDTO>> GetPerformance(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<PagedResponse<PerformanceDTO>>> GetPerformances(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
        Task<ServiceResponse> AddPerformance(PerformanceAddDTO performance, CancellationToken cancellationToken = default);
        Task<ServiceResponse> UpdatePerformance(PerformanceUpdateDTO performance, CancellationToken cancellationToken = default);
        Task<ServiceResponse> DeletePerformance(Guid id, CancellationToken cancellationToken = default);
    }
}
