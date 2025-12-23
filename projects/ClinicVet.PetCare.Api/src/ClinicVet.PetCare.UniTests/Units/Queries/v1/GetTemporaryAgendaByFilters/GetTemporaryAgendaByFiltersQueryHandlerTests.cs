using AutoMapper;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Services;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryAgendaByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters.Responses;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetTemporaryAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetTemporaryAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetTemporaryAgendaByFilters.Responses;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Queries.v1.GetTemporaryAgendaByFilters;

public sealed class GetTemporaryAgendaByFiltersQueryHandlerTests
{
    private readonly Mock<ILogger<GetTemporaryAgendaByFiltersQueryHandler>> _logger;
    private readonly IDomainContextNotifications _domainContextNotifications;
    private readonly Mock<ITemporaryAgendaRepository> _temporaryAgendaRepository;
    private readonly Mock<IMapper> _mapper;

    public GetTemporaryAgendaByFiltersQueryHandlerTests()
    {
        _logger = new Mock<ILogger<GetTemporaryAgendaByFiltersQueryHandler>>();
        _domainContextNotifications = new DomainContextNotifications();
        _temporaryAgendaRepository = new Mock<ITemporaryAgendaRepository>();
        _mapper = new Mock<IMapper>();
    }

    private GetTemporaryAgendaByFiltersQueryHandler EstablishContext()
    {
        return new GetTemporaryAgendaByFiltersQueryHandler(
            _logger.Object,
            _domainContextNotifications,
            _temporaryAgendaRepository.Object,
            _mapper.Object);
    }

    [Fact(DisplayName = "Should return valid response for temporary scheduling")]
    public async Task ShouldReturnValidResponseForTemporaryScheduling()
    {
        var query = GetTemporaryAgendaByFiltersQueryMock.GetDefaultInstance();
        var filterDto = GetAgendaByFiltersDtoMock.GetDefaultInstance();
        var temporaryAgendaDto = TemporaryAgendaDtoMock.GetDefaultInstances();
        var agendaDto = AgendaDtoMock.GetDefaultInstances();
        var temporaryAgendaDetail = GetTemporaryAgendaByFiltersQueryResponseDetailMock.GetDefaultInstances();

        _mapper
            .Setup(mapper => mapper.Map<GetAgendaByFiltersDto>(It.IsAny<GetTemporaryAgendaByFiltersQuery>()))
            .Returns(filterDto);

        _temporaryAgendaRepository
            .Setup(repository => repository.GetTemporaryAgendasAsync(It.IsAny<GetAgendaByFiltersDto>(), CancellationToken.None))
            .ReturnsAsync(temporaryAgendaDto);

        _mapper
            .Setup(mapper => mapper.Map<List<AgendaDto>>(It.IsAny<TemporaryAgendaDto>()))
            .Returns(agendaDto);

        _mapper
            .Setup(mapper => mapper.Map<List<GetTemporaryAgendaByFiltersQueryResponseDetail>>(It.IsAny<AgendaDto>()))
            .Returns(temporaryAgendaDetail);

        var response = await EstablishContext().Handle(query, CancellationToken.None);

        Assert.NotNull(response.Content);
    }

    [Fact(DisplayName = "Should return empty response when data not found")]
    public async Task ShouldReturnEmptyResponseWhenDataNotFound()
    {
        var query = GetTemporaryAgendaByFiltersQueryMock.GetDefaultInstance();
        var filterDto = GetAgendaByFiltersDtoMock.GetDefaultInstance();
        var temporaryAgendaDto = TemporaryAgendaDtoMock.GetEmptyInstances();

        _mapper
            .Setup(mapper => mapper.Map<GetAgendaByFiltersDto>(It.IsAny<GetTemporaryAgendaByFiltersQuery>()))
            .Returns(filterDto);

        _temporaryAgendaRepository
            .Setup(repository => repository.GetTemporaryAgendasAsync(It.IsAny<GetAgendaByFiltersDto>(), CancellationToken.None))
            .ReturnsAsync(temporaryAgendaDto);

        var response = await EstablishContext().Handle(query, CancellationToken.None);

        Assert.Null(response.Content);
    }
}