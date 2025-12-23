using ClinicVet.PetCare.Domain.Dtos.v1.CreateAgenda;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdateAgenda;
using ClinicVet.PetCare.Infra.Data.Services.Clients.v1;
using ClinicVet.PetCare.Infra.Data.Services.Models.v1;
using ClinicVet.PetCare.Infra.Data.Services.Services.v1;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.UnitTests.Mock.Dtos;
using ClinicVet.PetCare.UnitTests.Mock.Dtos.v1.CreateAgenda;
using ClinicVet.PetCare.UnitTests.Mock.Dtos.v1.UpdateAgenda;
using ClinicVet.PetCare.UnitTests.Units.Services.Helpers.v1;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;


namespace ClinicVet.PetCare.UnitTests.Units.Services.v1;

public sealed class AgendaServiceTests
{
    private readonly Mock<ILogger<AgendaService>> _logger;
    private readonly Mock<IAgendaClient> _agendaClient;
    private readonly Mock<IDistributedCache> _distributedCache;
    private readonly CacheSettings _cacheSettings;


    public AgendaServiceTests()
    {
        _logger = new Mock<ILogger<AgendaService>>();
        _agendaClient = new Mock<IAgendaClient>();
        _distributedCache = new Mock<IDistributedCache>();
        _cacheSettings = new CacheSettings { MinutesToExpireToken = 10 };
    }

    private AgendaService EstablishContext()
    {
        return new AgendaService(
            _logger.Object,
            _agendaClient.Object,
            _distributedCache.Object,
            _cacheSettings);
    }

    [Fact(DisplayName = "Should return valid scheduling response with cache")]
    public async Task ShouldReturnValidSchedulingResponse_WithCache()
    {
        var filters = GetAgendaByFiltersDtoMock.GetDefaultInstance();
        var agendaResponseDto = BaseAgendaDtoMock.GetDefaultInstance();
        var httpResponse = HttpResponseHelper.CreateJsonResponse(agendaResponseDto);

        _agendaClient
            .Setup(service => service.GetAgendaByFiltersAsync(It.IsAny<GetAgendaByFiltersDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(httpResponse);

        var result = await EstablishContext().GetAgendasAsync(filters, CancellationToken.None);

        Assert.NotNull(result);

        _agendaClient.Verify(service => service.GetAgendaByFiltersAsync(
            It.IsAny<GetAgendaByFiltersDto>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Should be possible to successfully register an appointment")]
    public async Task ShouldBePossibleToSuccessfullyRegisterAnAppointment()
    {
        var createAgendaDto = CreateAgendaDtoMock.GetDefaultInstance();
        const string CreateReturn = "Agendamento cadastrado com sucesso";

        var httpResponse = HttpResponseHelper.CreateJsonResponse(CreateReturn);

        _agendaClient
            .Setup(service => service.CreateAgendaAsync(It.IsAny<CreateAgendaDto>(), CancellationToken.None))
            .ReturnsAsync(httpResponse);

        var result = await EstablishContext().CreateAgendaAsync(createAgendaDto, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Should be possible to successfully update an appointment")]
    public async Task ShouldBePossibleToSuccessfullyUpdateAnAppointment()
    {
        var updateAgendaDto = UpdateAgendaDtoMock.GetDefaultInstance();
        const string UpdateReturn = "Agendamento atualizado com sucesso";

        var httpResponse = HttpResponseHelper.CreateJsonResponse(UpdateReturn);

        _agendaClient
            .Setup(service => service.UpdateAgendaAsync(It.IsAny<UpdateAgendaDto>(), CancellationToken.None))
            .ReturnsAsync(httpResponse);

        var result = await EstablishContext().UpdateAgendaAsync(updateAgendaDto, CancellationToken.None);

        Assert.NotNull(result);
    }
}