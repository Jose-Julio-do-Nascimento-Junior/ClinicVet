using ClinicVet.PetManager.Job.Domain.Commands.v1.PetManager;
using ClinicVet.PetManager.Job.Infra.Data.Services.Models.v1;
using Microsoft.Extensions.Options;
using Quartz;

namespace ClinicVet.PetManager.Job.Workers.v1;

public class PetManagerWorkerHandler : IJob
{
    private readonly IServiceProvider _provider;
    private readonly string _cronExpression;

    public PetManagerWorkerHandler(IServiceProvider provider, IOptions<ApplicationSettings> appSettings)
    {
        _cronExpression = appSettings.Value.Schedule;
        _provider = provider;
    }

    public  async Task Execute(IJobExecutionContext context)
    {
        await using var scope = _provider.CreateAsyncScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(new PetManagerCommand(), context.CancellationToken);
    }
}