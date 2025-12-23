using AutoMapper;
using ClinicVet.PetCare.Domain.Commands.v1.UpdatePetOwner;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.PetOwnerParameter;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.UpdatePetOwner;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.PetOwnerParameter;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.UpdatePetOwner;

public sealed class UpdatePetOwnerCommandHandlerTests
{
    private readonly Mock<ILogger<UpdatePetOwnerCommandHandler>> _logger;
    private readonly Mock<IPetOwnerRepository> _petOwnerRepository;
    private readonly Mock<IMapper> _mapper;

    public UpdatePetOwnerCommandHandlerTests()
    {
        _logger = new Mock<ILogger<UpdatePetOwnerCommandHandler>>();
        _petOwnerRepository = new Mock<IPetOwnerRepository>();
        _mapper = new Mock<IMapper>();
    }

    private UpdatePetOwnerCommandHandler EstablishContext()
    {
        return new UpdatePetOwnerCommandHandler(
            _logger.Object,
            _petOwnerRepository.Object,
            _mapper.Object);
    }

    [Fact(DisplayName = "Should update Pet Owner data successfully")]
    public async Task ShouldUpdatePetOwnerDataSuccessfully()
    {
        var command = UpdatePetOwnerCommandMock.GetDefaultInstance();
        var parametersDto = PetOwnerParametersDtoMock.GetDefaultInstance();
        const string UpdateReturn = "Atualização feita com sucesso.";

        _mapper
            .Setup(mapper => mapper.Map<UpdatePetOwnerCommand, PetOwnerParametersDto>(It.IsAny<UpdatePetOwnerCommand>()))
            .Returns(parametersDto);

        _petOwnerRepository
            .Setup(repository => repository.UpdatePetOwnerAsync(It.IsAny<PetOwnerParametersDto>(), CancellationToken.None))
            .ReturnsAsync(UpdateReturn);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(UpdateReturn, result.Content);

        _petOwnerRepository.Verify(repository => repository.UpdatePetOwnerAsync(It.IsAny<PetOwnerParametersDto>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Should return an error message when update fails")]
    public async Task ShouldReturnAnErrorMessageWhenUpdateFails()
    {
        var command = UpdatePetOwnerCommandMock.GetDefaultInstance();
        var parametersDto = PetOwnerParametersDtoMock.GetDefaultInstance();
        const string Return = "A atualização falhou.";

        _mapper
            .Setup(mapper => mapper.Map<UpdatePetOwnerCommand, PetOwnerParametersDto>(It.IsAny<UpdatePetOwnerCommand>()))
            .Returns(parametersDto);

        _petOwnerRepository
            .Setup(repository => repository.UpdatePetOwnerAsync(It.IsAny<PetOwnerParametersDto>(), CancellationToken.None))
            .ReturnsAsync(Return);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(Return, result.Content);

        _petOwnerRepository.Verify(repo => repo.UpdatePetOwnerAsync(It.IsAny<PetOwnerParametersDto>(), CancellationToken.None), Times.Once);
    }
}