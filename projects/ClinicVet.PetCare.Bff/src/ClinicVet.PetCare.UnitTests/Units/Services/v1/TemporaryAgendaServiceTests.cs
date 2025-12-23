using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Infra.Data.Services.Clients.v1;
using ClinicVet.PetCare.Infra.Data.Services.Services.v1;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.UnitTests.Mock.Dtos;
using ClinicVet.PetCare.UnitTests.Units.Services.Helpers.v1;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UnitTests.Units.Services.v1;

public sealed class TemporaryAgendaServiceTests
{
    private readonly Mock<ILogger<TemporaryAgendaService>> _logger;
    private readonly Mock<IAgendaClient> _agendaClient;

    public TemporaryAgendaServiceTests()
    {
        _logger = new Mock<ILogger<TemporaryAgendaService>>();
        _agendaClient = new Mock<IAgendaClient>();
    }

    private TemporaryAgendaService EstablishContext()
    {
        return new TemporaryAgendaService(
            _logger.Object,
            _agendaClient.Object);
    }

    [Fact(DisplayName = "Should return valid response for temporary scheduling")]
    public async Task ShouldReturnValidResponseForTemporaryScheduling()
    {
        var filters = GetAgendaByFiltersDtoMock.GetDefaultInstance();
        var agendaDto = BaseAgendaDtoMock.GetDefaultInstance();

        var httpResponse = HttpResponseHelper.CreateJsonResponse(agendaDto);

        _agendaClient
            .Setup(service => service.GetTemporaryAgendaByFiltersAsync(It.IsAny<GetAgendaByFiltersDto>(), CancellationToken.None))
            .ReturnsAsync(httpResponse);

        var result = await EstablishContext().GetTemporaryAgendasAsync(filters, CancellationToken.None);

        Assert.NotNull(result);
    }
}