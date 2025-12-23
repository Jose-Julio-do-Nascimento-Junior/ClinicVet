using AutoMapper;
using ClinicVet.PetCare.Domain.Commands.v1.CreatePet;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.PetParameter;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.CreatePet;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.PetParameter;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.CreatePet;

public sealed class CreatePetCommandHandlerTests
{
    private readonly Mock<ILogger<CreatePetCommandHandler>> _logger;
    private readonly Mock<IPetRepository> _petRepository;
    private readonly Mock<IMapper> _mapper;

    public CreatePetCommandHandlerTests()
    {
        _logger = new Mock<ILogger<CreatePetCommandHandler>>();
        _petRepository = new Mock<IPetRepository>();
        _mapper = new Mock<IMapper>();
    }

    private CreatePetCommandHandler EstablishContext()
    {
        return new CreatePetCommandHandler(
            _logger.Object,
            _petRepository.Object,
            _mapper.Object);
    }

    [Fact(DisplayName = "should be successfully register pet")]
    public async Task ShouldBeSuccessfullyRegisterPet()
    {
        var command = CreatePetCommandMock.GetDefaultInstance();
        var parametersDto = PetParameterDtoMock.GetDefaultInstance();
        const string CreateReturn = "Pet cadastrado com sucesso.";

        _mapper
            .Setup(mapper => mapper.Map<PetParameterDto>(It.IsAny<CreatePetCommand>()))
            .Returns(parametersDto);

        _petRepository
            .Setup(repository => repository.CreatePetAsync(It.IsAny<PetParameterDto>(), CancellationToken.None))
            .ReturnsAsync(CreateReturn);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(CreateReturn, result.Content);

        _petRepository.Verify(repository => repository.CreatePetAsync(It.IsAny<PetParameterDto>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Should return an error message when registration fails")]
    public async Task ShouldReturnAnErrorMessageWhenRegistrationFails()
    {
        var command = CreatePetCommandMock.GetDefaultInstance();
        var parametersDto = PetParameterDtoMock.GetDefaultInstance();
        const string Return = "Erro ao cadastrar pet";

        _mapper
            .Setup(mapper => mapper.Map<PetParameterDto>(It.IsAny<CreatePetCommand>()))
            .Returns(parametersDto);

        _petRepository
            .Setup(repository => repository.CreatePetAsync(It.IsAny<PetParameterDto>(), CancellationToken.None))
            .ReturnsAsync(Return);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(Return, result.Content);

        _petRepository.Verify(repository => repository.CreatePetAsync(It.IsAny<PetParameterDto>(), CancellationToken.None), Times.Once);
    }
}