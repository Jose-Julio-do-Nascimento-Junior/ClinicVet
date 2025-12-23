using AutoMapper;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.PetOwnerParameter;
using ClinicVet.PetCare.Domain.Resources.v1;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Domain.Commands.v1.UpdatePetOwner;

public sealed class UpdatePetOwnerCommandHandler : CommandHandler<UpdatePetOwnerCommand>
{
    private const string HandlerName = nameof(UpdatePetOwnerCommandHandler);

    private readonly ILogger<UpdatePetOwnerCommandHandler> _logger;
    private readonly IPetOwnerRepository _petOwnerRepository;
    private readonly IMapper _mapper;

    public UpdatePetOwnerCommandHandler(
        
        ILogger<UpdatePetOwnerCommandHandler> logger, 
        IPetOwnerRepository petOwnerRepository,
        IMapper mapper)
    {
        _logger = logger;
        _petOwnerRepository = petOwnerRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(UpdatePetOwnerCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var petOwnerParameters = _mapper.Map<UpdatePetOwnerCommand, PetOwnerParametersDto>(command);

        var petOwnerResponse = await _petOwnerRepository.UpdatePetOwnerAsync( petOwnerParameters, cancellationToken);

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new Response() { Content = petOwnerResponse };
    }
}