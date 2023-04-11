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
    public interface ITransactionService
    {
        Task<ServiceResponse<PagedResponse<TransactionDTO>>> GetAllTransactionsAsync(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    }
}
