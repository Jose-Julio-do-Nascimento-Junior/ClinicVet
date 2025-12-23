using ClinicVet.PetCare.Domain.Dtos.v1.CreatePet;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePet;
using ClinicVet.PetCare.Infra.Data.Services.Clients.v1;
using ClinicVet.PetCare.Infra.Data.Services.Models.v1;
using ClinicVet.PetCare.Infra.Data.Services.Services.v1;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.UnitTests.Mock.Dtos;
using ClinicVet.PetCare.UnitTests.Mock.Dtos.v1.CreatePet;
using ClinicVet.PetCare.UnitTests.Mock.Dtos.v1.UpdatePet;
using ClinicVet.PetCare.UnitTests.Units.Services.Helpers.v1;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UnitTests.Units.Services.v1;

public sealed class PetServiceTests
{
    private readonly Mock<ILogger<PetService>> _logger;
    private readonly Mock<IPetClient> _petClient;
    private readonly Mock<IDistributedCache> _distributedCache;
    private readonly CacheSettings _cacheSettings;


    public PetServiceTests()
    {
        _logger = new Mock<ILogger<PetService>>();
        _petClient = new Mock<IPetClient>();
        _distributedCache = new Mock<IDistributedCache>();
        _cacheSettings = new CacheSettings { MinutesToExpireToken = 10 };

    }

    private PetService EstablishContext()
    {
        return new PetService(
            _logger.Object,
            _petClient.Object,
            _distributedCache.Object,
            _cacheSettings);
    }

    [Fact(DisplayName = "Should return a valid pet response")]
    public async Task ShouldReturnValidPetResponse()
    {
        var filters = PetByFiltersDtoMock.GetDefaultInstance();
        var petDto = BasePetDtoMock.GetDefaultInstance();

        var httpResponse = HttpResponseHelper.CreateJsonResponse(petDto);

        _petClient
             .Setup(service => service.GetPetsByFiltersAsync(It.IsAny<PetByFiltersDto>(), CancellationToken.None))
             .ReturnsAsync(httpResponse);

        var result = await EstablishContext().GetPetsAsync(filters, CancellationToken.None);

        Assert.NotNull(result);

        _petClient.Verify(service => service.GetPetsByFiltersAsync(
              It.IsAny<PetByFiltersDto>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "should be successfully register pet")]
    public async Task ShouldBeSuccessfullyRegisterPet()
    {
        var createPetDto = CreatePetDtoMock.GetDefaultInstance();
        const string CreateReturn = "Pet cadastrado com sucesso.";

        var httpResponse = HttpResponseHelper.CreateJsonResponse(CreateReturn);

        _petClient
             .Setup(service => service.CreatePetAsync(It.IsAny<CreatePetDto>(), CancellationToken.None))
             .ReturnsAsync(httpResponse);

        var result = await EstablishContext().CreatePetAsync(createPetDto, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Should be successfully update pet")]
    public async Task ShouldBeSuccessfullyUpdatePet()
    {
        var updatePetDto = UpdatePetDtoMock.GetDefaultInstance();
        const string UpdateReturn = "Pet atualizado com sucesso";

        var httpResponse = HttpResponseHelper.CreateJsonResponse(UpdateReturn);

        _petClient
             .Setup(service => service.UpdatePetAsync(It.IsAny<UpdatePetDto>(), CancellationToken.None))
             .ReturnsAsync(httpResponse);

        var result = await EstablishContext().UpdatePetAsync(updatePetDto, CancellationToken.None);

        Assert.NotNull(result);
    }
}