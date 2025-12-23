using AutoMapper;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Services;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters.Responses;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetByFilters.Response;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Queries.v1.GetPetByFilters;

public sealed class GetPetByFiltersQueryHandlerTests
{
    private readonly Mock<ILogger<GetPetByFiltersQueryHandler>> _logger;
    private readonly IDomainContextNotifications _domainContextNotifications;
    private readonly Mock<IPetRepository> _petRepository;
    private readonly Mock<IMapper> _mapper;

    public GetPetByFiltersQueryHandlerTests()
    {
        _logger = new Mock<ILogger<GetPetByFiltersQueryHandler>>();
        _domainContextNotifications = new DomainContextNotifications();
        _petRepository = new Mock<IPetRepository>();
        _mapper = new Mock<IMapper>();
    }

    private GetPetByFiltersQueryHandler EstablishContext()
    {
        return new GetPetByFiltersQueryHandler(
            _logger.Object,
            _domainContextNotifications,
            _petRepository.Object,
            _mapper.Object);
    }

    [Fact(DisplayName = "Should return valid pet response")]
    public async Task ShouldReturnValidPetResponse()
    {
        var query = GetPetByFiltersQueryMock.GetDefaultInstance();
        var filterDto = PetByFiltersDtoMock.GetDefaultInstance();
        var petDto = PetDtoMock.GetDefaultInstances();
        var resposneDetail = GetPetByFiltersQueryResponseDetailMock.GetDefaultInstances();

        _mapper
            .Setup(mapper => mapper.Map<PetByFiltersDto>(It.IsAny<GetPetByFiltersQuery>()))
            .Returns(filterDto);

        _petRepository
            .Setup(repository => repository.GetPetsAsync(It.IsAny<PetByFiltersDto>(), CancellationToken.None))
            .ReturnsAsync(petDto);

        _mapper
            .Setup(mapper => mapper.Map<List<GetPetByFiltersQueryResponseDetail>>(It.IsAny<PetDto>()))
            .Returns(resposneDetail);

        var response = await EstablishContext().Handle(query, CancellationToken.None);

        Assert.NotNull(response.Content);
    }

    [Fact(DisplayName = "Should return empty response when data not found")]
    public async Task ShouldBeMShouldReturnEmptyResponseWhenDataNotFound()
    {
        var query = GetPetByFiltersQueryMock.GetDefaultInstance();
        var filterDto = PetByFiltersDtoMock.GetDefaultInstance();
        var petDto = PetDtoMock.GetEmptyInstances();

        _mapper
            .Setup(mapper => mapper.Map<PetByFiltersDto>(It.IsAny<GetPetByFiltersQuery>()))
            .Returns(filterDto);

        _petRepository
            .Setup(repository => repository.GetPetsAsync(It.IsAny<PetByFiltersDto>(), CancellationToken.None))
            .ReturnsAsync(petDto);

        var response = await EstablishContext().Handle(query, CancellationToken.None);

        Assert.Null(response.Content);
    }
}