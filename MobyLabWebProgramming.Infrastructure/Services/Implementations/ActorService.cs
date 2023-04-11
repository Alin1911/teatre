using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
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

        public async Task<ServiceResponse<PagedResponse<ActorDTO>>> GetAllActorsAsync(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
        {
            var result = await _repository.PageAsync(pagination, new ActorProjectionSpec(pagination.Search), cancellationToken);

            return ServiceResponse<PagedResponse<ActorDTO>>.ForSuccess(result);
        }
    }
}
