using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    public class TicketController : AuthorizedController
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService, IUserService userService) : base(userService)
        {
            _ticketService = ticketService;
        }
    }
}
