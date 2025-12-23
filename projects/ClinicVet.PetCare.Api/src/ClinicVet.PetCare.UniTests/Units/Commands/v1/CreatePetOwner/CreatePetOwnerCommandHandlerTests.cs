using AutoMapper;
using ClinicVet.PetCare.Domain.Commands.v1.CreatePetOwner;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.PetOwnerParameter;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.CreatePetOwner;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.PetOwnerParameter;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.CreatePetOwner;

public sealed class CreatePetOwnerCommandHandlerTests
{
    private readonly Mock<ILogger<CreatePetOwnerCommandHandler>> _logger;
    private readonly Mock<IPetOwnerRepository> _petOwnerRepository;
    private readonly Mock<IMapper> _mapper;

    public CreatePetOwnerCommandHandlerTests()
    {
        _logger = new Mock<ILogger<CreatePetOwnerCommandHandler>>();
        _petOwnerRepository = new Mock<IPetOwnerRepository>();
        _mapper = new Mock<IMapper>();
    }

    private CreatePetOwnerCommandHandler EstablishContext()
    {
        return new CreatePetOwnerCommandHandler(
           _logger.Object,
           _petOwnerRepository.Object,
           _mapper.Object);
    }

    [Fact(DisplayName = "should be successfully register pet owner")]
    public async Task ShouldBeSuccessfullyRegisterPetOwner()
    {
        var command = CreatePetOwnerCommandMock.GetDefaultInstance();
        var parametersDto = PetOwnerParametersDtoMock.GetDefaultInstance();
        const string CreateReturn = "Usuário cadastrado com sucesso.";

        _mapper
            .Setup(mapper => mapper.Map<CreatePetOwnerCommand, PetOwnerParametersDto>(It.IsAny<CreatePetOwnerCommand>()))
            .Returns(parametersDto);

        _petOwnerRepository
            .Setup(repository => repository.CreatePetOwnerAsync(It.IsAny<PetOwnerParametersDto>(), CancellationToken.None))
            .ReturnsAsync(CreateReturn);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(CreateReturn, result.Content);

        _petOwnerRepository.Verify(repository => repository.CreatePetOwnerAsync(It.IsAny<PetOwnerParametersDto>(), CancellationToken.None),Times.Once);
    }

    [Fact(DisplayName = "Should return an error message when registration fails")]
    public async Task ShouldReturnAnErrorMessageWhenRegistrationFails()
    {
        var command = CreatePetOwnerCommandMock.GetDefaultInstance();
        var parametersDto = PetOwnerParametersDtoMock.GetDefaultInstance();
        const string Return = "Erro ao Cadastrar.";

        _mapper
            .Setup(mapper => mapper.Map<CreatePetOwnerCommand, PetOwnerParametersDto>(It.IsAny<CreatePetOwnerCommand>()))
            .Returns(parametersDto);

        _petOwnerRepository
            .Setup(repository => repository.CreatePetOwnerAsync(It.IsAny<PetOwnerParametersDto>(), CancellationToken.None))
            .ReturnsAsync(Return);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(Return, result.Content);

        _petOwnerRepository.Verify(repository => repository.CreatePetOwnerAsync(It.IsAny<PetOwnerParametersDto>(), CancellationToken.None), Times.Once);
    }
}