using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Workers;

/// <summary>
/// This is an example of a worker service, this service is called on the applications start to do some asynchronous work.
/// </summary>
public class InitializerWorker : BackgroundService
{
    private readonly ILogger<InitializerWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public InitializerWorker(ILogger<InitializerWorker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger; // The logger instance is injected here.
        _serviceProvider = serviceProvider; // Here the service provider is injected to request other components on runtime at request.
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            await using var scope = _serviceProvider.CreateAsyncScope(); // Here a new scope is created, this is useful to get new scoped instances.
            var userService = scope.ServiceProvider.GetService<IUserService>(); // Here an instance for a service is requested, it may fail if the component is not declared or
                                                                                // an exception is thrown on it’s creation.

            if (userService == null)
            {
                _logger.LogInformation("Couldn't create the user service!");

                return;
            }

            var count = await userService.GetUserCount(cancellationToken);

            if (count.Result == 0)
            {
                _logger.LogInformation("No user found, adding default user!");

                await userService.AddUser(new()
                {
                    Email = "admin@default.com",
                    Name = "Admin",
                    Role = UserRoleEnum.Admin,
                    Password = PasswordUtils.HashPassword("default")
                }, cancellationToken: cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing database!");
        }
    }
}