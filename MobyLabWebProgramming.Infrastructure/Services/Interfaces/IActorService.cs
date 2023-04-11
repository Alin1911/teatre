using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Requests;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    public interface IActorService
    {
        Task<ServiceResponse<PagedResponse<ActorDTO>>> GetAllActorsAsync(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    }
}
