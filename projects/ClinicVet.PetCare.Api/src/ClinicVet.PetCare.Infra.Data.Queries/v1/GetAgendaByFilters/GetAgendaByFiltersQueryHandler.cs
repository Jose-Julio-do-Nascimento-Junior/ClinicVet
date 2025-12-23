using AutoMapper;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Fixed;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters.Responses;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters;

public sealed class GetAgendaByFiltersQueryHandler : QueryHandler<GetAgendaByFiltersQuery>
{
    private const string HandlerName = nameof(GetAgendaByFiltersQueryHandler);

    private readonly ILogger<GetAgendaByFiltersQueryHandler> _logger;
    private readonly IDomainContextNotifications _domainContextNotifications;
    private readonly IAgendaRepository _agendaRepository;
    private readonly IMapper _mapper;

    public GetAgendaByFiltersQueryHandler(
        ILogger<GetAgendaByFiltersQueryHandler> logger,
        IDomainContextNotifications domainContextNotifications,
        IAgendaRepository agendaRepository, IMapper mapper)
    {
        _logger = logger;
        _domainContextNotifications = domainContextNotifications;
        _agendaRepository = agendaRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(GetAgendaByFiltersQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var filters = _mapper.Map<GetAgendaByFiltersDto>(query);

        var agendaRepository = await _agendaRepository.GetAgendasAsync(filters, cancellationToken);

        if (!agendaRepository.Any())
        {
            _domainContextNotifications.Add(Message.NotFoundAgenda, NotificationType.NotFound);
            _logger.LogInformation(LogTemplate.EndHandler, HandlerName, Message.NotFoundAgenda);
            return new Response();
        }

        var responseDetails = _mapper.Map<List<GetAgendaByFiltersQueryResponseDetail>>(agendaRepository);

        var response = new GetAgendaByFiltersQueryResponse(responseDetails, agendaRepository.Count());

       _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new() { Content = response };
    }
}