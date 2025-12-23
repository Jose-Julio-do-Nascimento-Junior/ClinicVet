using ClinicVet.AgendaStatus.Job.Domain.Commands.v1.UpdateAgendaStatus;
using ClinicVet.AgendaStatus.Job.Domain.Contracts.v1.Repositories;
using ClinicVet.AgendaStatus.Job.UnitTests.Mock.Commands.v1.UpdateAgendaStatus;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.AgendaStatus.Job.UnitTests.Units.Commands.v1.UpdateAgendaStatus;

public sealed class UpdateAgendaStatusCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateAgendaStatusCommandHandler>> _logger;
    private readonly Mock<IUpdateAgendaStatusRepository> _updateAgendaStatus;

    public UpdateAgendaStatusCommandHandlerTests()
    {
        _logger = new Mock<ILogger<UpdateAgendaStatusCommandHandler>>();
        _updateAgendaStatus = new Mock<IUpdateAgendaStatusRepository>();
    }

    private UpdateAgendaStatusCommandHandler EstablishContext()
    {
        return new UpdateAgendaStatusCommandHandler(
            _logger.Object,
            _updateAgendaStatus.Object);
    }

    [Fact(DisplayName = "Should complete agenda status update flow without exceptions")]
    public async Task ShouldCompleteAgendaStatusUpdateFlowWithoutExceptions()
    {
        var command = UpdateAgendaStatusCommandMock.GetDefaultInstance();

        _updateAgendaStatus
             .Setup(repository => repository.UpdateAgendaStatusAsync(CancellationToken.None))
             .Returns(Task.CompletedTask);

        var exception = await Record.ExceptionAsync(() => EstablishContext().Handle(command, CancellationToken.None));

        Assert.Null(exception);

        _updateAgendaStatus.Verify(repository => repository.UpdateAgendaStatusAsync(CancellationToken.None), Times.Once());
    }

    [Fact(DisplayName = "Should be null content for success flow")]
    public async Task ShouldBeNullContentForSuccessFlow()
    {
        var command = UpdateAgendaStatusCommandMock.GetDefaultInstance();

        _updateAgendaStatus
             .Setup(repository => repository.UpdateAgendaStatusAsync(CancellationToken.None))
             .Returns(Task.CompletedTask);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Null(result.Content);
    }
}