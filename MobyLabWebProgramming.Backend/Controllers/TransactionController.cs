using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    public class TransactionController : AuthorizedController
        {
            private readonly ITransactionService _transactionService;

            public TransactionController(ITransactionService transactionService, IUserService userService) : base(userService)
            {
                _transactionService = transactionService;
            }

            // Implementați metodele CRUD pentru Ticket
        }
    }
