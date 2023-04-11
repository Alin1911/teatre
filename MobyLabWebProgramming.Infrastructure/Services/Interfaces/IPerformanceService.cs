using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    public interface IPerformanceService
    {
        Task<ServiceResponse<PagedResponse<PerformanceDTO>>> GetAllPerformancesAsync(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    }
}
