using AutoMapper;
using ClinicVet.PetCare.Domain.Commands.v1.UpdatePet;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.PetParameter;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.UpdatePet;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.PetParameter;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.UpdatePet;

public sealed class UpdatePetCommandHandlerTests
{
    private readonly Mock<ILogger<UpdatePetCommandHandler>> _logger;
    private readonly Mock<IPetRepository> _petRepository;
    private readonly Mock<IMapper> _mapper;

    public UpdatePetCommandHandlerTests()
    {
        _logger = new Mock<ILogger<UpdatePetCommandHandler>>();
        _petRepository = new Mock<IPetRepository>();
        _mapper = new Mock<IMapper>();
    }

    private UpdatePetCommandHandler EstablishContext()
    {
        return new UpdatePetCommandHandler(
            _logger.Object,
            _petRepository.Object,
            _mapper.Object);
    }

    [Fact(DisplayName = "Should be successfully update pet")]
    public async Task ShouldBeSuccessfullyUpdatePet()
    {
        var command = UpdatePetCommandMock.GetDefaultInstance();
        var parametersDto = PetParameterDtoMock.GetDefaultInstance();
        const string UpdateReturn = "Pet atualizado com sucesso";

        _mapper
            .Setup(mapper => mapper.Map<PetParameterDto>(It.IsAny<UpdatePetCommand>()))
            .Returns(parametersDto);

        _petRepository
            .Setup(repository => repository.UpdatePetAsync(It.IsAny<PetParameterDto>(), CancellationToken.None))
            .ReturnsAsync(UpdateReturn);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(UpdateReturn, result.Content);

        _petRepository.Verify(repository => repository.UpdatePetAsync(It.IsAny<PetParameterDto>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Should return an error message when update fails")]
    public async Task ShouldReturnAnErrorMessageWhenUpdateFails()
    {
        var command = UpdatePetCommandMock.GetDefaultInstance();
        var parametersDto = PetParameterDtoMock.GetDefaultInstance();
        const string Return = "Erro ao atualizar pet";

        _mapper
            .Setup(mapper => mapper.Map<PetParameterDto>(It.IsAny<UpdatePetCommand>()))
            .Returns(parametersDto);

        _petRepository
            .Setup(repository => repository.UpdatePetAsync(It.IsAny<PetParameterDto>(), CancellationToken.None))
            .ReturnsAsync(Return);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(Return, result.Content);

        _petRepository.Verify(repository => repository.UpdatePetAsync(It.IsAny<PetParameterDto>(), CancellationToken.None), Times.Once);
    }
}