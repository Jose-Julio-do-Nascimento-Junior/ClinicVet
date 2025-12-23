using AutoMapper;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Services;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters.Responses;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetOwnerByFilters.Responses;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Queries.v1.GetPetOwnerByFilters;

public sealed class GetPetOwnerByFiltersQueryHandlerTests
{
    private readonly Mock<ILogger<GetPetOwnerByFiltersQueryHandler>> _logger;
    private readonly IDomainContextNotifications _domainContextNotifications;
    private readonly Mock<IPetOwnerRepository> _petOwnerRepository;
    private readonly Mock<IMapper> _mapper;

    public GetPetOwnerByFiltersQueryHandlerTests()
    {
        _logger = new Mock<ILogger<GetPetOwnerByFiltersQueryHandler>>();
        _domainContextNotifications = new DomainContextNotifications();
        _petOwnerRepository = new Mock<IPetOwnerRepository>();
        _mapper = new Mock<IMapper>();
    }

    private GetPetOwnerByFiltersQueryHandler EstablishContext()
    {
        return new GetPetOwnerByFiltersQueryHandler(
            _logger.Object,
            _domainContextNotifications,
            _petOwnerRepository.Object,
            _mapper.Object);
    }

    [Fact(DisplayName = "Should return a valid pet owner response")]
    public async Task ShouldReturnValidPetOwnerResponse()
    {
        var query = GetPetOwnerByFiltersQueryMock.GetDefaultInstance();
        var filterDto = PetOwnerByFiltersDtoMock.GetDefaultInstance();
        var petOwnerDto = PetOwnerDtoMock.GetDefaultInstances();
        var resposneDetail = GetPetOwnerByFiltersQueryResponseDetailMock.GetDefaultInstances();

        _mapper
            .Setup(mapper => mapper.Map<PetOwnerByFiltersDto>(It.IsAny<GetPetOwnerByFiltersQuery>()))
            .Returns(filterDto);

        _petOwnerRepository
            .Setup(repository => repository.GetPetOwnersAsync(It.IsAny<PetOwnerByFiltersDto>(), CancellationToken.None))
            .ReturnsAsync(petOwnerDto);

        _mapper
            .Setup(mapper => mapper.Map<List<GetPetOwnerByFiltersQueryResponseDetail>>(It.IsAny<PetOwnerDto>()))
            .Returns(resposneDetail);

        var response = await EstablishContext().Handle(query, CancellationToken.None);

        Assert.NotNull(response.Content);
    }

    [Fact(DisplayName = "Should return empty response when data not found")]
    public async Task ShouldBeMShouldReturnEmptyResponseWhenDataNotFound()
    {
        var query = GetPetOwnerByFiltersQueryMock.GetDefaultInstance();
        var filterDto = PetOwnerByFiltersDtoMock.GetDefaultInstance();
        var petOwnerDto = PetOwnerDtoMock.GetEmptyInstances();

        _mapper
            .Setup(mapper => mapper.Map<PetOwnerByFiltersDto>(It.IsAny<GetPetOwnerByFiltersQuery>()))
            .Returns(filterDto);

        _petOwnerRepository
            .Setup(repository => repository.GetPetOwnersAsync(It.IsAny<PetOwnerByFiltersDto>(), CancellationToken.None))
            .ReturnsAsync(petOwnerDto);

        var response = await EstablishContext().Handle(query, CancellationToken.None);

        Assert.Null(response.Content);
    }
}