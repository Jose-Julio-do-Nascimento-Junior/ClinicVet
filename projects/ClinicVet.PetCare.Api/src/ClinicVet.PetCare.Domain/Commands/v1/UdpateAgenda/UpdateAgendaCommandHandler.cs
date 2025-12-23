using AutoMapper;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.AgendaParameter;
using ClinicVet.PetCare.Domain.Resources.v1;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Domain.Commands.v1.UdpateAgenda;

public sealed class UpdateAgendaCommandHandler : CommandHandler<UpdateAgendaCommand>
{
    private const string HandlerName = nameof(UpdateAgendaCommandHandler);

    private readonly ILogger<UpdateAgendaCommandHandler> _logger;
    private readonly IAgendaRepository _agendaRepository;
    private readonly IMapper _mapper;

    public UpdateAgendaCommandHandler(
        ILogger<UpdateAgendaCommandHandler> logger, 
        IAgendaRepository agendaRepository, 
        IMapper mapper)
    {
        _logger = logger;
        _agendaRepository = agendaRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(UpdateAgendaCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var agendaParameters = _mapper.Map<AgendaParameterDto>(command);

        var agendaResponse = await _agendaRepository.UpdateAgendaAsync(agendaParameters, cancellationToken);

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new Response() { Content = agendaResponse };
    }
}