using AutoMapper;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.PetOwnerParameter;
using ClinicVet.PetCare.Domain.Resources.v1;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Domain.Commands.v1.CreatePetOwner;

public sealed class CreatePetOwnerCommandHandler : CommandHandler<CreatePetOwnerCommand>
{
    private const string HandlerName = nameof(CreatePetOwnerCommandHandler);

    private readonly ILogger<CreatePetOwnerCommandHandler> _logger;
    private readonly IPetOwnerRepository _petOwnerRepository;
    private readonly IMapper _mapper;

    public CreatePetOwnerCommandHandler(
        ILogger<CreatePetOwnerCommandHandler> logger, 
        IPetOwnerRepository petOwnerRepository, 
        IMapper mapper)
    {
        _logger = logger;
        _petOwnerRepository = petOwnerRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(CreatePetOwnerCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var petOwnerParameters = _mapper.Map<PetOwnerParametersDto>(command);

        var petOwnerResponse = await _petOwnerRepository.CreatePetOwnerAsync(petOwnerParameters, cancellationToken);

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new Response() { Content = petOwnerResponse };
    }
}