using AutoMapper;
using ClinicVet.PetCare.Domain.Commands.v1.CreateAgenda;
using ClinicVet.PetCare.Domain.Commands.v1.UdpateAgenda;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.AgendaParameter;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.UdpateAgenda;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.AgendaParameter;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.UdpateAgenda;

public sealed class UpdateAgendaCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateAgendaCommandHandler>> _logger;
    private readonly Mock<IAgendaRepository> _agendaRepository;
    private readonly Mock<IMapper> _mapper;

    public UpdateAgendaCommandHandlerTests()
    {
        _logger = new Mock<ILogger<UpdateAgendaCommandHandler>>();
        _agendaRepository = new Mock<IAgendaRepository>();
        _mapper = new Mock<IMapper>();
    }

    private UpdateAgendaCommandHandler EstablishContext()
    {
        return new UpdateAgendaCommandHandler(
            _logger.Object,
            _agendaRepository.Object,
            _mapper.Object);
    }

    [Fact(DisplayName = "Should be possible to successfully update an appointment")]
    public async Task ShouldBePossibleToSuccessfullyUpdateAnAppointment()
    {
        var command = UpdateAgendaCommandMock.GetDefaultInstance();
        var parametersDto = AgendaParameterDtoMock.GetDefaultInstance();
        const string UpdateReturn = "Agendamento atualizado com sucesso";

        _mapper
            .Setup(mapper => mapper.Map<AgendaParameterDto>(It.IsAny<CreateAgendaCommand>()))
            .Returns(parametersDto);

        _agendaRepository
            .Setup(repository => repository.UpdateAgendaAsync(It.IsAny<AgendaParameterDto>(), CancellationToken.None))
            .ReturnsAsync(UpdateReturn);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(UpdateReturn, result.Content);

        _agendaRepository.Verify(repository => repository.UpdateAgendaAsync(It.IsAny<AgendaParameterDto>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Should return an error message when update fails")]
    public async Task ShouldReturnAnErrorMessageWhenUpdateFails()
    {
        var command = UpdateAgendaCommandMock.GetDefaultInstance();
        var parametersDto = AgendaParameterDtoMock.GetDefaultInstance();
        const string Return = "Erro ao atualizar agendamento";

        _mapper
            .Setup(mapper => mapper.Map<AgendaParameterDto>(It.IsAny<CreateAgendaCommand>()))
            .Returns(parametersDto);

        _agendaRepository
            .Setup(repository => repository.UpdateAgendaAsync(It.IsAny<AgendaParameterDto>(), CancellationToken.None))
            .ReturnsAsync(Return);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Equal(Return, result.Content);

        _agendaRepository.Verify(repository => repository.UpdateAgendaAsync(It.IsAny<AgendaParameterDto>(), CancellationToken.None), Times.Once);
    }
}