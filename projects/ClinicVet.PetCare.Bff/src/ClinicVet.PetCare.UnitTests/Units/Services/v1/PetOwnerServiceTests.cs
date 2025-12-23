using ClinicVet.PetCare.Domain.Dtos.v1;
using ClinicVet.PetCare.Domain.Dtos.v1.CreatePetOwner;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePetOwner;
using ClinicVet.PetCare.Infra.Data.Services.Clients.v1;
using ClinicVet.PetCare.Infra.Data.Services.Models.v1;
using ClinicVet.PetCare.Infra.Data.Services.Services.v1;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.UnitTests.Mock.Dtos.v1.CreatePetOwner;
using ClinicVet.PetCare.UnitTests.Mock.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.UnitTests.Mock.Dtos.v1.UpdatePetOwner;
using ClinicVet.PetCare.UnitTests.Units.Services.Helpers.v1;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UnitTests.Units.Services.v1;

public sealed class PetOwnerServiceTests
{
    private readonly Mock<ILogger<PetOwnerService>> _logger;
    private readonly Mock<IPetOwnerClient> _petOwnerClient;
    private readonly Mock<IDistributedCache> _distributedCache;
    private readonly CacheSettings _cacheSettings;


    public PetOwnerServiceTests()
    {
        _logger = new Mock<ILogger<PetOwnerService>>();
        _petOwnerClient = new Mock<IPetOwnerClient>();
        _distributedCache = new Mock<IDistributedCache>();
        _cacheSettings = new CacheSettings { MinutesToExpireToken = 10 };
    }

    private PetOwnerService EstablishContext()
    {
        return new PetOwnerService(
            _logger.Object,
            _petOwnerClient.Object,
            _distributedCache.Object,
            _cacheSettings);
    }

    [Fact(DisplayName = "Should return valid pet owner response")]
    public async Task ShouldReturnValidPetOwnerResponse()
    {
        var filters = PetOwnerByFiltersDtoMock.GetDefaultInstance();
        var petOwnerDto = PetOwnerDtoMock.GetDefaultInstance();

        var httpResponse = HttpResponseHelper.CreateJsonResponse(petOwnerDto);

        _petOwnerClient
             .Setup(service => service.GetPetOwnerByFiltersAsync(It.IsAny<PetOwnerByFiltersDto>(), CancellationToken.None))
             .ReturnsAsync(httpResponse);

        var result = await EstablishContext().GetPetOwnersAsync(filters, CancellationToken.None);

        Assert.NotNull(result);

        _petOwnerClient.Verify(service => service.GetPetOwnerByFiltersAsync(
            It.IsAny<PetOwnerByFiltersDto>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "should be successfully register pet owner")]
    public async Task ShouldBeSuccessfullyRegisterPetOwner()
    {
        var createPetOwnerDto = CreatePetOwnerDtoMock.GetDefaultInstance();
        const string CreateReturn = "Usuário cadastrado com sucesso.";

        var httpResponse = HttpResponseHelper.CreateJsonResponse(CreateReturn);

        _petOwnerClient
             .Setup(service => service.CreatePetOwnerAsync(It.IsAny<CreatePetOwnerDto>(), CancellationToken.None))
             .ReturnsAsync(httpResponse);

        var result = await EstablishContext().CreatePetOwnerAsync(createPetOwnerDto, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Should update Pet Owner data successfully")]
    public async Task ShouldUpdatePetOwnerDataSuccessfully()
    {
        var updatePetOwnerDto = UpdatePetOwnerDtoMock.GetDefaultInstance();
        const string UpdateReturn = "Atualização feita com sucesso.";

        var httpResponse = HttpResponseHelper.CreateJsonResponse(UpdateReturn);

        _petOwnerClient
            .Setup(service => service.UpdatePetOwnerAsync(It.IsAny<UpdatePetOwnerDto>(), CancellationToken.None))
            .ReturnsAsync(httpResponse);

        var result = await EstablishContext().UpdatePetOwnerAsync(updatePetOwnerDto, CancellationToken.None);

        Assert.NotNull(result);
    }
}