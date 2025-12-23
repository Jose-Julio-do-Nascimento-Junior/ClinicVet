using AutoMapper;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Fixed;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters.Responses;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters;

public sealed class GetPetByFiltersQueryHandler : QueryHandler<GetPetByFiltersQuery>
{
    private const string HandlerName = nameof(GetPetByFiltersQueryHandler);

    private readonly ILogger<GetPetByFiltersQueryHandler> _logger;
    private readonly IDomainContextNotifications _domainContextNotifications;
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;

    public GetPetByFiltersQueryHandler(
        ILogger<GetPetByFiltersQueryHandler> logger,
        IDomainContextNotifications domainContextNotifications,
        IPetRepository petRepository,
        IMapper mapper)
    {
        _logger = logger;
        _domainContextNotifications = domainContextNotifications;
        _petRepository = petRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(GetPetByFiltersQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var filters = _mapper.Map<PetByFiltersDto>(query);

        var petRepository = await _petRepository.GetPetsAsync(filters,cancellationToken);

        if (!petRepository.Any())
        {
            _domainContextNotifications.Add(Message.NotFoundPet, NotificationType.NotFound);
            _logger.LogInformation(LogTemplate.EndHandler, HandlerName, Message.NotFoundPet);
            return new Response();
        }

        var responseDetails = _mapper.Map<List<GetPetByFiltersQueryResponseDetail>>(petRepository);

        var response = new GetPetByFiltersQueryResponse(responseDetails, petRepository.Count());

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new() { Content = response };
    }
}