using ClinicVet.PetManager.Job.Domain.Commands.v1.PetManager;
using ClinicVet.PetManager.Job.Domain.Contracts.v1.Repositories;
using ClinicVet.PetManager.Job.UniTests.Mock.Commands.v1.PetManager;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetManager.Job.UniTests.Units.Commands.v1.PetManager;

public sealed class PetManagerCommandHandlerTests
{

    private readonly Mock<ILogger<PetManagerCommandHandler>> _logger;
    private readonly Mock<IPetManageRepository> _petManageRepository;
    private readonly Mock<IAgendaManagerRepository> _agendaManagerRepository;
    private readonly Mock<IDeleteAgendaAndPetDataRepository> _deleteAgendaAndPet;

    public PetManagerCommandHandlerTests()
    {
        _logger = new Mock<ILogger<PetManagerCommandHandler>>();
        _petManageRepository = new Mock<IPetManageRepository>();
        _agendaManagerRepository = new Mock<IAgendaManagerRepository>();
        _deleteAgendaAndPet = new Mock<IDeleteAgendaAndPetDataRepository>();
    }

    private PetManagerCommandHandler EstablishContext()
    {
        return new PetManagerCommandHandler(
           _logger.Object,
           _petManageRepository.Object,
           _agendaManagerRepository.Object,
           _deleteAgendaAndPet.Object);
    }

    [Fact(DisplayName = "Should must complete the operations related to scheduling and pets without exception")]
    public async Task ShouldMustCompleteTheOperationsRelatedToSchedulingAndPetsWithoutException()
    {
        var command = PetManagerCommandMock.GetDefaultInstance();
        const int rowsInserted = 1;

        _agendaManagerRepository
             .Setup(agendaRepository => agendaRepository.AgendaManagerAsync(CancellationToken.None))
             .ReturnsAsync(rowsInserted);

        _petManageRepository
             .Setup(petRepository => petRepository.PetManagerAsync(CancellationToken.None))
             .ReturnsAsync(rowsInserted);

        _deleteAgendaAndPet
             .Setup(repository => repository.DeleteAgendaAndPetDataAsync(CancellationToken.None))
             .Returns(Task.CompletedTask);

        var exception = await Record.ExceptionAsync(() => EstablishContext().Handle(command, CancellationToken.None));

        Assert.Null(exception);

        _agendaManagerRepository.Verify(agendaRepository => agendaRepository.AgendaManagerAsync(CancellationToken.None), Times.Once());

        _petManageRepository.Verify(petRepository => petRepository.PetManagerAsync(CancellationToken.None), Times.Once());

        _deleteAgendaAndPet.Verify(repository => repository.DeleteAgendaAndPetDataAsync(CancellationToken.None), Times.Once());
    }

    [Fact(DisplayName = "Should be null content for success flow")]
    public async Task ShouldBeNullContentForSuccessFlow()
    {
        var command = PetManagerCommandMock.GetDefaultInstance();
        const int rowsInserted = 0;

        _agendaManagerRepository
             .Setup(agendaRepository => agendaRepository.AgendaManagerAsync(CancellationToken.None))
             .ReturnsAsync(rowsInserted);

        _petManageRepository
             .Setup(petRepository => petRepository.PetManagerAsync(CancellationToken.None))
             .ReturnsAsync(rowsInserted);

        _deleteAgendaAndPet
             .Setup(repository => repository.DeleteAgendaAndPetDataAsync(CancellationToken.None))
             .Returns(Task.CompletedTask);

        var result = await EstablishContext().Handle(command, CancellationToken.None);

        Assert.Null(result.Content);
    }
}