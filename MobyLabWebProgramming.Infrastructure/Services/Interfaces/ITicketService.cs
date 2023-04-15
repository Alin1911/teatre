using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;


namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ITicketService
{
    Task<ServiceResponse<TicketDTO>> GetTicket(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceResponse<PagedResponse<TicketDTO>>> GetTickets(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    Task<ServiceResponse> AddTicket(TicketAddDTO ticket, CancellationToken cancellationToken = default);
    Task<ServiceResponse> UpdateTicket(TicketUpdateDTO ticket, CancellationToken cancellationToken = default);
    Task<ServiceResponse> DeleteTicket(Guid id, CancellationToken cancellationToken = default);
}