using AutoMapper;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Fixed;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters.Responses;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters;

public sealed class GetTemporaryPetByFiltersQueryHandler : QueryHandler<GetTemporaryPetByFiltersQuery>
{
    private const string HandlerName = nameof(GetTemporaryPetByFiltersQueryHandler);

    private readonly ILogger<GetTemporaryPetByFiltersQueryHandler> _logger;
    private readonly IDomainContextNotifications _domainContextNotifications;
    private readonly ITemporaryPetRepository _temporaryPetRepository;
    private readonly IMapper _mapper;

    public GetTemporaryPetByFiltersQueryHandler(
        ILogger<GetTemporaryPetByFiltersQueryHandler> logger,
        IDomainContextNotifications domainContextNotifications,
        ITemporaryPetRepository temporaryPetRepository,
        IMapper mapper)
    {
        _logger = logger;
        _domainContextNotifications = domainContextNotifications;
        _temporaryPetRepository = temporaryPetRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(GetTemporaryPetByFiltersQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var filters = _mapper.Map<PetByFiltersDto>(query);

        var temporaryPetRepository = await _temporaryPetRepository.GetTemporaryPetsAsync(filters, cancellationToken);

        if (!temporaryPetRepository.Any())
        {
            _domainContextNotifications.Add(Message.NotFoundPet, NotificationType.NotFound);
            _logger.LogInformation(LogTemplate.EndHandler, HandlerName, Message.NotFoundPet);
            return new Response();
        }

        var petRepository = _mapper.Map<List<PetDto>>(temporaryPetRepository);

        var responseDetails = _mapper.Map<List<GetTemporaryPetByFiltersQueryResponsesDetail>>(petRepository);

        var response = new GetTemporaryPetByFiltersQueryResponses(responseDetails, temporaryPetRepository.Count());

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new() { Content = response };
    }
}