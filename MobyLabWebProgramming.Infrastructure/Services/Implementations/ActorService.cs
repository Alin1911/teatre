using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MobyLabWebProgramming.Infrastructure.Services.Implementations
{
    public class ActorService : IActorService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;


        public ActorService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<ActorDTO>> GetActor(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetAsync(new ActorProjectionSpec(id), cancellationToken);

            return result != null ?
                ServiceResponse<ActorDTO>.ForSuccess(result) :
                ServiceResponse<ActorDTO>.FromError(CommonErrors.ActorNotFound);
        }

        public async Task<ServiceResponse<PagedResponse<ActorDTO>>> GetActors(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
        {
            var result = await _repository.PageAsync(pagination, new ActorProjectionSpec(pagination.Search), cancellationToken);

            return ServiceResponse<PagedResponse<ActorDTO>>.ForSuccess(result);
        }

        public async Task<ServiceResponse> AddActor(ActorAddDTO actor, CancellationToken cancellationToken = default)
        {
            var newActor = new Actor
            {
                Nume = actor.Nume,
                Prenume = actor.Prenume,
                DataNastere = actor.DataNastere,
                Salariu = actor.Salariu,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(newActor, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdateActor(ActorUpdateDTO actor, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(new ActorSpec(actor.Id), cancellationToken);

            if (entity != null)
            {
                entity.Nume = actor.Nume ?? entity.Nume;
                entity.Prenume = actor.Prenume ?? entity.Prenume;
                entity.DataNastere = actor.DataNastere ?? entity.DataNastere;
                entity.Salariu = actor.Salariu ?? entity.Salariu;
                entity.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(entity, cancellationToken);
            }

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeleteActor(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync<Actor>(id, cancellationToken);

            return ServiceResponse.ForSuccess();
        }
    }
}
