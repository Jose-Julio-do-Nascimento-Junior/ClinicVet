using AutoMapper;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Services;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters.Responses;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetAgendaByFilters.Response;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Queries.v1.GetAgendaByFilters;

public sealed class GetAgendaByFiltersQueryHandlerTests
{
    private readonly Mock<ILogger<GetAgendaByFiltersQueryHandler>> _logger;
    private readonly IDomainContextNotifications _domainContextNotifications;
    private readonly Mock<IAgendaRepository> _agendaRepository;
    private readonly Mock<IMapper> _mapper;

    public GetAgendaByFiltersQueryHandlerTests()
    {
        _logger = new Mock<ILogger<GetAgendaByFiltersQueryHandler>>();
        _domainContextNotifications = new DomainContextNotifications();
        _agendaRepository = new Mock<IAgendaRepository>();
        _mapper = new Mock<IMapper>();
    }

    private GetAgendaByFiltersQueryHandler EstablishContext()
    {
        return new GetAgendaByFiltersQueryHandler(
            _logger.Object,
            _domainContextNotifications,
            _agendaRepository.Object,
            _mapper.Object);
    }

    [Fact(DisplayName = "Should return valid scheduling response")]
    public async Task ShouldReturnValidSchedulingResponse()
    {
        var query = GetAgendaByFiltersQueryMock.GetDefaultInstance();
        var filterDto = GetAgendaByFiltersDtoMock.GetDefaultInstance();
        var agendaDto = AgendaDtoMock.GetDefaultInstances();
        var responseDetail = GetAgendaByFiltersQueryResponseDetailMock.GetDefaultInstances();

        _mapper
            .Setup(mapper => mapper.Map<GetAgendaByFiltersDto>(It.IsAny<GetAgendaByFiltersQuery>()))
            .Returns(filterDto);

        _agendaRepository
            .Setup(repository => repository.GetAgendasAsync(It.IsAny<GetAgendaByFiltersDto>(), CancellationToken.None))
            .ReturnsAsync(agendaDto);

        _mapper
            .Setup(mapper => mapper.Map<List<GetAgendaByFiltersQueryResponseDetail>>(It.IsAny<AgendaDto>()))
            .Returns(responseDetail);

        var response = await EstablishContext().Handle(query, CancellationToken.None);

        Assert.NotNull(response.Content);
    }

    [Fact(DisplayName = "Should return empty response when data not found")]
    public async Task ShouldReturnEmptyResponseWhenDataNotFound()
    {
        var query = GetAgendaByFiltersQueryMock.GetDefaultInstance();
        var filterDto = GetAgendaByFiltersDtoMock.GetDefaultInstance();
        var agendaDto = AgendaDtoMock.GetEmptyInstances();

        _mapper
            .Setup(mapper => mapper.Map<GetAgendaByFiltersDto>(It.IsAny<GetAgendaByFiltersQuery>()))
            .Returns(filterDto);

        _agendaRepository
            .Setup(repository => repository.GetAgendasAsync(It.IsAny<GetAgendaByFiltersDto>(), CancellationToken.None))
            .ReturnsAsync(agendaDto);

        var response = await EstablishContext().Handle(query, CancellationToken.None);

        Assert.Null(response.Content);
    }
}