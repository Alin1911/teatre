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
    public class PerformanceService : IPerformanceService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public PerformanceService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<PerformanceDTO>> GetPerformance(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetAsync(new PerformanceProjectionSpec(id), cancellationToken);

            return result != null ?
                ServiceResponse<PerformanceDTO>.ForSuccess(result) :
                ServiceResponse<PerformanceDTO>.FromError(CommonErrors.PerformanceNotFound);
        }

        public async Task<ServiceResponse<PagedResponse<PerformanceDTO>>> GetPerformances(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
        {
            var result = await _repository.PageAsync(pagination, new PerformanceProjectionSpec(pagination.Search), cancellationToken);

            return ServiceResponse<PagedResponse<PerformanceDTO>>.ForSuccess(result);
        }

        public async Task<ServiceResponse> AddPerformance(PerformanceAddDTO performance, CancellationToken cancellationToken = default)
        {
            var newPerformance = new Performance
            {
                IdPiesa = performance.IdPiesa,
                StartDate = performance.StartDate,
                EndDate = performance.EndDate,
                HallId = performance.HallId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(newPerformance, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdatePerformance(PerformanceUpdateDTO performance, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(new PerformanceSpec(performance.Id), cancellationToken);

            if (entity != null)
            {
                entity.IdPiesa = performance.IdPiesa ?? entity.IdPiesa;
                entity.StartDate = performance.StartDate ?? entity.StartDate;
                entity.EndDate = performance.EndDate ?? entity.EndDate;
                entity.HallId = performance.HallId ?? entity.HallId;
                entity.HallId = performance.HallId ?? entity.HallId;
                entity.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(entity, cancellationToken);
            }

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeletePerformance(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync<Performance>(id, cancellationToken);

            return ServiceResponse.ForSuccess();
        }
    }
}