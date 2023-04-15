using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public TicketService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<TicketDTO>> GetTicket(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetAsync(new TicketProjectionSpec(id), cancellationToken);

            return result != null ?
                ServiceResponse<TicketDTO>.ForSuccess(result) :
                ServiceResponse<TicketDTO>.FromError(CommonErrors.TicketNotFound);
        }

        public async Task<ServiceResponse<PagedResponse<TicketDTO>>> GetTickets(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
        {
            var result = await _repository.PageAsync(pagination, new TicketProjectionSpec(pagination.Search), cancellationToken);

            return ServiceResponse<PagedResponse<TicketDTO>>.ForSuccess(result);
        }

        public async Task<ServiceResponse> AddTicket(TicketAddDTO ticket, CancellationToken cancellationToken = default)
        {
            var newTicket = new Ticket
            {
                PerformanceId = ticket.PerformanceId,
                UserId = ticket.UserId,
                TransactionId = ticket.TransactionId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(newTicket, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdateTicket(TicketUpdateDTO ticket, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(new TicketSpec(ticket.Id), cancellationToken);

            if (entity != null)
            {
                entity.PerformanceId = ticket.PerformanceId ?? entity.PerformanceId;
                entity.UserId = ticket.UserId ?? entity.UserId;
                entity.TransactionId = ticket.TransactionId ?? entity.TransactionId;
                entity.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(entity, cancellationToken);
            }

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeleteTicket(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync<Ticket>(id, cancellationToken);

            return ServiceResponse.ForSuccess();
        }
    }
}