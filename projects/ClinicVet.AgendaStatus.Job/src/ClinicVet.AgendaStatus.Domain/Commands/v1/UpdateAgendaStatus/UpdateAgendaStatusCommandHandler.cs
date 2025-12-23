using ClinicVet.AgendaStatus.Job.Domain.Contracts.v1.Repositories;
using ClinicVet.AgendaStatus.Job.Domain.Resources.v1;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ClinicVet.AgendaStatus.Job.Domain.Commands.v1.UpdateAgendaStatus;

public sealed class UpdateAgendaStatusCommandHandler : CommandHandler<UpdateAgendaStatusCommand>
{
    private const string HandlerName = nameof(UpdateAgendaStatusCommandHandler);

    private readonly ILogger<UpdateAgendaStatusCommandHandler> _logger;
    private readonly IUpdateAgendaStatusRepository _updateAgendaStatus;


    public UpdateAgendaStatusCommandHandler(
        ILogger<UpdateAgendaStatusCommandHandler> logger,
       IUpdateAgendaStatusRepository updateAgendaStatus)
    {
        _logger = logger;
        _updateAgendaStatus = updateAgendaStatus;
    }

    public override async Task<Response> Handle(UpdateAgendaStatusCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        await _updateAgendaStatus.UpdateAgendaStatusAsync(cancellationToken);

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new Response();
    }
}