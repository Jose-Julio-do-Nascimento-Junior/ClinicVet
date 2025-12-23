using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Infra.Data.Services.Clients.v1;
using ClinicVet.PetCare.Infra.Data.Services.Services.v1;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.UnitTests.Mock.Dtos;
using ClinicVet.PetCare.UnitTests.Units.Services.Helpers.v1;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UnitTests.Units.Services.v1;

public sealed class TemporaryPetServiceTests
{
    private readonly Mock<ILogger<TemporaryPetService>> _logger;
    private readonly Mock<IPetClient> _petClient;

    public TemporaryPetServiceTests()
    {
        _logger = new Mock<ILogger<TemporaryPetService>>();
        _petClient = new Mock<IPetClient>();
    }

    private TemporaryPetService EstablishContext()
    {
        return new TemporaryPetService(
            _logger.Object,
            _petClient.Object);
    }

    [Fact(DisplayName = "Should return valid response for temporary pets")]
    public async Task ShouldReturnValidResponseForTemporaryPets()
    {
        var filters = PetByFiltersDtoMock.GetDefaultInstance();
        var petDto = BasePetDtoMock.GetDefaultInstance();

        var httpResponse = HttpResponseHelper.CreateJsonResponse(petDto);

        _petClient
             .Setup(service => service.GetTemporaryPetsByFiltersAsync(It.IsAny<PetByFiltersDto>(), CancellationToken.None))
             .ReturnsAsync(httpResponse);

        var result = await EstablishContext().GetTemporaryPetsAsync(filters, CancellationToken.None);

        Assert.NotNull(result);
    }
}