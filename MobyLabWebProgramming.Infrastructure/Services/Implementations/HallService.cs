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

    public class HallService : IHallService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public HallService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<HallDTO>> GetHall(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetAsync(new HallProjectionSpec(id), cancellationToken);

            return result != null ?
                ServiceResponse<HallDTO>.ForSuccess(result) :
                ServiceResponse<HallDTO>.FromError(CommonErrors.HallNotFound);
        }

        public async Task<ServiceResponse<PagedResponse<HallDTO>>> GetHalls(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
        {
            var result = await _repository.PageAsync(pagination, new HallProjectionSpec(pagination.Search), cancellationToken);

            return ServiceResponse<PagedResponse<HallDTO>>.ForSuccess(result);
        }

        public async Task<ServiceResponse> AddHall(HallAddDTO hall, CancellationToken cancellationToken = default)
        {
            var newHall = new Hall
            {
                Nume = hall.Nume,
                Capacity = hall.Capacity,
                AdministrationCost = hall.AdministrationCost,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(newHall, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdateHall(HallUpdateDTO hall, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(new HallSpec(hall.Id), cancellationToken);

            if (entity != null)
            {
                entity.Nume = hall.Nume ?? entity.Nume;
                entity.Capacity = hall.Capacity ?? entity.Capacity;
                entity.AdministrationCost = hall.AdministrationCost ?? entity.AdministrationCost;
                entity.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(entity, cancellationToken);
            }

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeleteHall(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync<Hall>(id, cancellationToken);

            return ServiceResponse.ForSuccess();
        }
    }
}
