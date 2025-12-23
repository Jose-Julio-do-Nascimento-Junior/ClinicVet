using AutoMapper;
using ClinicVet.PetCare.Domain.Commands.v1.CreateAgenda;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.AgendaParameter;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.CreateAgenda;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.AgendaParameter;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.CreateAgenda;

public sealed class CreateAgendaCommandHandlerTests
{
    private readonly Mock<ILogger<CreateAgendaCommandHandler>> _logger;
    private readonly Mock<IAgendaRepository> _agendaRepository;
    private readonly Mock<IMapper> _mapper;

    public CreateAgendaCommandHandlerTests()
    {
        _logger = new Mock<ILogger<CreateAgendaCommandHandler>>();
        _agendaRepository = new Mock<IAgendaRepository>();
        _mapper = new Mock<IMapper>();
    }

    private CreateAgendaCommandHandler EstablishContext()
    {
        return new CreateAgendaCommandHandler(
            _logger.Object,
            _agendaRepository.Object,
            _mapper.Object);
    }

    [Fact(DisplayName = "Should be possible to successfully register an appointment")]
    public async Task ShouldBePossibleToSuccessfullyRegisterAnAppointment()
    {
        var command = CreateAgendaCommandMock.GetDefaultInstance();
        var parametersDto = AgendaParameterDtoMock.GetDefaultInstance();
        const string CreateReturn = "Agendamento cadastrado com sucesso";

        _mapper
            .Setup(mapper => mapper.Map<AgendaParameterDto>(It.IsAny<CreateAgendaCommand>()))
            .Returns(parametersDto);

        _agendaRepository
            .Setup(repository => repository.CreateAgendaAsync(It.IsAny<AgendaParameterDto>(), CancellationToken.None))
            .ReturnsAsync(CreateReturn);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(CreateReturn, result.Content);

        _agendaRepository.Verify(repository => repository.CreateAgendaAsync(It.IsAny<AgendaParameterDto>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Should return an error message when registration fails")]
    public async Task ShouldReturnAnErrorMessageWhenRegistrationFails()
    {
        var command = CreateAgendaCommandMock.GetDefaultInstance();
        var parametersDto = AgendaParameterDtoMock.GetDefaultInstance();
        const string Return = "Erro ao cadastrar agendamento";

        _mapper
            .Setup(mapper => mapper.Map<AgendaParameterDto>(It.IsAny<CreateAgendaCommand>()))
            .Returns(parametersDto);

        _agendaRepository
            .Setup(repository => repository.CreateAgendaAsync(It.IsAny<AgendaParameterDto>(), CancellationToken.None))
            .ReturnsAsync(Return);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(Return, result.Content);

        _agendaRepository.Verify(repository => repository.CreateAgendaAsync(It.IsAny<AgendaParameterDto>(), CancellationToken.None), Times.Once);
    }
}