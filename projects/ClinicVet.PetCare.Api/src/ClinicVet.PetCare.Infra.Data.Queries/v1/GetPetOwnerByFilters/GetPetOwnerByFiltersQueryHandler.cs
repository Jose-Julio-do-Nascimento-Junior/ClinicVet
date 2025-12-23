using AutoMapper;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Fixed;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters.Responses;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters;

public sealed class GetPetOwnerByFiltersQueryHandler : QueryHandler<GetPetOwnerByFiltersQuery>
{
    private const string HandlerName = nameof(GetPetOwnerByFiltersQueryHandler);

    private readonly ILogger<GetPetOwnerByFiltersQueryHandler> _logger;
    private readonly IDomainContextNotifications _domainContextNotifications;
    private readonly IPetOwnerRepository _petOwnerRepository;
    private readonly IMapper _mapper;

    public GetPetOwnerByFiltersQueryHandler(
        ILogger<GetPetOwnerByFiltersQueryHandler> logger,
        IDomainContextNotifications domainContextNotifications,
        IPetOwnerRepository petOwnerRepository,
        IMapper mapper)
    {
        _logger = logger;
        _domainContextNotifications = domainContextNotifications;
        _petOwnerRepository = petOwnerRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(GetPetOwnerByFiltersQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var filters = _mapper.Map<PetOwnerByFiltersDto>(query);

        var petOwnerRepository = await _petOwnerRepository.GetPetOwnersAsync(filters, cancellationToken);

        if (!petOwnerRepository.Any())
        {
            _domainContextNotifications.Add(Message.NotFoundPetOwner, NotificationType.NotFound);
            _logger.LogInformation(LogTemplate.EndHandler, HandlerName, Message.NotFoundPetOwner);
            return new Response();
        }

        var responseDetails = _mapper.Map<List<GetPetOwnerByFiltersQueryResponseDetail>>(petOwnerRepository);

        var response = new GetPetOwnerByFiltersQueryResponse(responseDetails, petOwnerRepository.Count());

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new() { Content = response };
    }
}