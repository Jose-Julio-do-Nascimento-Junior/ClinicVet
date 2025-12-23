using AutoMapper;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Fixed;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters.Responses;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters;

public sealed class GetTemporaryAgendaByFiltersQueryHandler : QueryHandler<GetTemporaryAgendaByFiltersQuery>
{
    private const string HandlerName = nameof(GetTemporaryAgendaByFiltersQueryHandler);
    
    private readonly ILogger<GetTemporaryAgendaByFiltersQueryHandler> _logger;
    private readonly IDomainContextNotifications _domainContextNotifications;
    private readonly ITemporaryAgendaRepository _temporaryAgendaRepository;
    private readonly IMapper _mapper;

    public GetTemporaryAgendaByFiltersQueryHandler(
        ILogger<GetTemporaryAgendaByFiltersQueryHandler> logger,
        IDomainContextNotifications domainContextNotifications,
        ITemporaryAgendaRepository temporaryAgendaRepository,
        IMapper mapper)
    {
        _logger = logger;
        _domainContextNotifications = domainContextNotifications;
        _temporaryAgendaRepository = temporaryAgendaRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(GetTemporaryAgendaByFiltersQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var filters = _mapper.Map<GetAgendaByFiltersDto>(query);

        var temporaryAgendaRepository = await _temporaryAgendaRepository.GetTemporaryAgendasAsync(filters, cancellationToken);

        if (!temporaryAgendaRepository.Any())
        {
            _domainContextNotifications.Add(Message.NotFoundAgenda, NotificationType.NotFound);
            _logger.LogInformation(LogTemplate.EndHandler, HandlerName, Message.NotFoundAgenda);
            return new Response();
        }

        var agendaRepository = _mapper.Map<List<AgendaDto>>(temporaryAgendaRepository);

        var responseDetails = _mapper.Map<List<GetTemporaryAgendaByFiltersQueryResponseDetail>>(agendaRepository);

        var response = new GetTemporaryAgendaByFiltersQueryResponse(responseDetails, temporaryAgendaRepository.Count());

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new() { Content = response };
    }
}