using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    public class PerformanceController : AuthorizedController
    {
        private readonly IPerformanceService _performanceService;

        public PerformanceController(IPerformanceService performanceService, IUserService userService) : base(userService)
        {
            _performanceService = performanceService;
        }

    }
}
