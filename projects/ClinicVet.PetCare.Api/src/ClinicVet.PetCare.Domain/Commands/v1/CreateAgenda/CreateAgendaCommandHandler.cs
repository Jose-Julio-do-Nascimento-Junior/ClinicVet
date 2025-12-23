using AutoMapper;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.AgendaParameter;
using ClinicVet.PetCare.Domain.Resources.v1;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Domain.Commands.v1.CreateAgenda;

public sealed class CreateAgendaCommandHandler : CommandHandler<CreateAgendaCommand>
{
    private const string HandlerName = nameof(CreateAgendaCommandHandler);

    private readonly ILogger<CreateAgendaCommandHandler> _logger;
    private readonly IAgendaRepository _agendaRepository;
    private readonly IMapper _mapper;

    public CreateAgendaCommandHandler(
        ILogger<CreateAgendaCommandHandler> logger,
        IAgendaRepository agendaRepository,
        IMapper mapper)
    {
        _logger = logger;
        _agendaRepository = agendaRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(CreateAgendaCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var agendaParameters = _mapper.Map<AgendaParameterDto>(command);

        var agendaResponse = await _agendaRepository.CreateAgendaAsync(agendaParameters, cancellationToken);

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new Response() { Content = agendaResponse };
    }
}