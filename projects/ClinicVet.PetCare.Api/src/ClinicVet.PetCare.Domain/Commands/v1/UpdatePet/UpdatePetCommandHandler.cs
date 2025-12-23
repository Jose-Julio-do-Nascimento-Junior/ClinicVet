using AutoMapper;
using ClinicVet.Core.Domain.Handlers;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.PetParameter;
using ClinicVet.PetCare.Domain.Resources.v1;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Domain.Commands.v1.UpdatePet;

public sealed class UpdatePetCommandHandler : CommandHandler<UpdatePetCommand>
{
    private const string HandlerName = nameof(UpdatePetCommandHandler);

    private readonly ILogger<UpdatePetCommandHandler> _logger;
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;

    public UpdatePetCommandHandler(
        ILogger<UpdatePetCommandHandler> logger,
        IPetRepository petRepository, 
        IMapper mapper)
    {
        _logger = logger;
        _petRepository = petRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(UpdatePetCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, HandlerName);

        var petParameters = _mapper.Map<PetParameterDto>(command);

        var petResponse = await _petRepository.UpdatePetAsync(petParameters, cancellationToken);

        _logger.LogInformation(LogTemplate.EndHandler, HandlerName, string.Empty);

        return new Response() { Content = petResponse };
    }
}