using AutoMapper;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Services;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryPetByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters.Responses;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetTemporaryPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetTemporaryPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetTemporaryPetByFilters.Responses;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Queries.v1.GetTemporaryPetByFilters;

public sealed class GetTemporaryPetByFiltersQueryHandlerTests
{
    private readonly Mock<ILogger<GetTemporaryPetByFiltersQueryHandler>> _logger;
    private readonly IDomainContextNotifications _domainContextNotifications;
    private readonly Mock<ITemporaryPetRepository> _temporaryPetRepository;
    private readonly Mock<IMapper> _mapper;

    public GetTemporaryPetByFiltersQueryHandlerTests()
    {
        _logger = new Mock<ILogger<GetTemporaryPetByFiltersQueryHandler>>();
        _domainContextNotifications = new DomainContextNotifications();
        _temporaryPetRepository = new Mock<ITemporaryPetRepository>();
        _mapper = new Mock<IMapper>();
    }

    private GetTemporaryPetByFiltersQueryHandler EstablishContext()
    {
        return new GetTemporaryPetByFiltersQueryHandler(
            _logger.Object,
            _domainContextNotifications,
            _temporaryPetRepository.Object,
            _mapper.Object);
    }

    [Fact(DisplayName = "Should return valid response for temporary pets")]
    public async Task ShouldReturnValidResponseForTemporaryPets()
    {
        var query = GetTemporaryPetByFiltersQueryMock.GetDefaultInstance();
        var filterDto = PetByFiltersDtoMock.GetDefaultInstance();
        var temporaryPetDto = TemporaryPetDtoMock.GetDefaultInstances();
        var petDto = PetDtoMock.GetDefaultInstances();
        var temporaryPetDetail = GetTemporaryPetByFiltersQueryResponsesDetailMock.GetDefaultInstances();

        _mapper
            .Setup(mapper => mapper.Map<PetByFiltersDto>(It.IsAny<GetTemporaryPetByFiltersQuery>()))
            .Returns(filterDto);

        _temporaryPetRepository
            .Setup(repository => repository.GetTemporaryPetsAsync(It.IsAny<PetByFiltersDto>(), CancellationToken.None))
            .ReturnsAsync(temporaryPetDto);

        _mapper
            .Setup(mapper => mapper.Map<List<PetDto>>(It.IsAny<TemporaryPetDto>()))
            .Returns(petDto);

        _mapper
            .Setup(mapper => mapper.Map<List<GetTemporaryPetByFiltersQueryResponsesDetail>>(It.IsAny<PetDto>()))
            .Returns(temporaryPetDetail);

        var response = await EstablishContext().Handle(query, CancellationToken.None);

        Assert.NotNull(response.Content);
    }

    [Fact(DisplayName = "Should return empty response when data not found")]
    public async Task ShouldReturnEmptyResponseWhenDataNotFound()
    {
        var query = GetTemporaryPetByFiltersQueryMock.GetDefaultInstance();
        var filterDto = PetByFiltersDtoMock.GetDefaultInstance();
        var temporaryPetDto = TemporaryPetDtoMock.GetEmptyInstances();
        
        _mapper
            .Setup(mapper => mapper.Map<PetByFiltersDto>(It.IsAny<GetTemporaryPetByFiltersQuery>()))
            .Returns(filterDto);

        _temporaryPetRepository
            .Setup(repository => repository.GetTemporaryPetsAsync(It.IsAny<PetByFiltersDto>(), CancellationToken.None))
            .ReturnsAsync(temporaryPetDto);

        var response = await EstablishContext().Handle(query, CancellationToken.None);

        Assert.Null(response.Content);
    }
}