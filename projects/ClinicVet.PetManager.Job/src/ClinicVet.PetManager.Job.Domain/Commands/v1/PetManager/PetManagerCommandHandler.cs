using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetManager.Job.Domain.Contracts.v1.Repositories;
using ClinicVet.PetManager.Job.Domain.Resources.v1;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetManager.Job.Domain.Commands.v1.PetManager;

public sealed class PetManagerCommandHandler : CommandHandler<PetManagerCommand>
{
    private const string HandlerName = nameof(PetManagerCommandHandler);

    private readonly ILogger<PetManagerCommandHandler> _logger;
    private readonly IPetManageRepository _petManageRepository;
    private readonly IAgendaManagerRepository _agendaManagerRepository;
    private readonly IDeleteAgendaAndPetDataRepository _deleteAgendaAndPet;

    public PetManagerCommandHandler(
        ILogger<PetManagerCommandHandler> logger,
        IPetManageRepository petManageRepository,
        IAgendaManagerRepository agendaManagerRepository,
        IDeleteAgendaAndPetDataRepository deleteAgendaAndPet)
    {
        _logger = logger;
        _petManageRepository = petManageRepository;
        _agendaManagerRepository = agendaManagerRepository;
        _deleteAgendaAndPet = deleteAgendaAndPet;
    }

    public override async Task<Response> Handle(PetManagerCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var agendaInserted = await _agendaManagerRepository.AgendaManagerAsync(cancellationToken);

        var petInserted = await _petManageRepository.PetManagerAsync(cancellationToken);

        if (agendaInserted > Constants.RowsInserted || petInserted > Constants.RowsInserted)
        {
            await _deleteAgendaAndPet.DeleteAgendaAndPetDataAsync(cancellationToken);
        }

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new Response();
    }
}