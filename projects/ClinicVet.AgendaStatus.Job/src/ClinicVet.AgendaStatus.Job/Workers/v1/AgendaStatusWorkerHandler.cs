using ClinicVet.AgendaStatus.Job.Domain.Commands.v1.UpdateAgendaStatus;
using ClinicVet.AgendaStatus.Job.Infra.Data.Services.Models.v1;
using Microsoft.Extensions.Options;
using Quartz;

namespace ClinicVet.AgendaStatus.Job.Workers.v1;

public sealed class AgendaStatusWorkerHandler : IJob
{
    private readonly IServiceProvider _provider;
    private readonly string _cronExpression;

    public AgendaStatusWorkerHandler(IServiceProvider provider, IOptions<ApplicationSettings> appSettings)
    {
        _cronExpression = appSettings.Value.Schedule;
        _provider = provider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await using var scope = _provider.CreateAsyncScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(new UpdateAgendaStatusCommand(), context.CancellationToken);
    }
}