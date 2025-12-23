using AutoMapper;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.PetParameter;
using ClinicVet.PetCare.Domain.Resources.v1;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Domain.Commands.v1.CreatePet;

public sealed class CreatePetCommandHandler : CommandHandler<CreatePetCommand>
{
    private const string HandlerName = nameof(CreatePetCommandHandler);

    private readonly ILogger<CreatePetCommandHandler> _logger;
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;

    public CreatePetCommandHandler(
        ILogger<CreatePetCommandHandler> logger, 
        IPetRepository petRepository, 
        IMapper mapper)
    {
        _logger = logger;
        _petRepository = petRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(CreatePetCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var petParameters = _mapper.Map<PetParameterDto>(command);

        var petResponse = await _petRepository.CreatePetAsync(petParameters, cancellationToken);

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new Response() { Content = petResponse };
    }
}