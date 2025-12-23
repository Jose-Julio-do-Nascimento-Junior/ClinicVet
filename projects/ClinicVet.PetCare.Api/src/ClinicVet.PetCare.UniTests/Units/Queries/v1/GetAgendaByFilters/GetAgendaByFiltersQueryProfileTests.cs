using AutoMapper;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetAgendaByFilters.Response;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Queries.v1.GetAgendaByFilters;

public sealed class GetAgendaByFiltersQueryProfileTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetAgendaByFiltersQueryProfile>());
        return config.CreateMapper();
    }

    [Fact(DisplayName = "Should map valid parameters in filters")]
    public void ShouldMapValidParametersInFilters()
    {
        var mapper = CreateMapper();
        var query = GetAgendaByFiltersQueryMock.GetDefaultInstance();
        var filterDto = GetAgendaByFiltersDtoMock.GetDefaultInstance();

        mapper.Map(query, filterDto);

        Assert.True(query.Document == filterDto.Document);
        Assert.True(query.Skip == filterDto.Skip);
        Assert.True(query.Take == filterDto.Take);
    }

    [Fact(DisplayName = "Should map valid parameters")]
    public void ShouldMapValidParameters()
    {
        var mapper = CreateMapper();
        var agendaDto = AgendaDtoMock.GetDefaultInstance();
        var responseDetail = GetAgendaByFiltersQueryResponseDetailMock.GetDefaultInstance();

        mapper.Map(agendaDto, responseDetail);

        Assert.True(agendaDto.Pet == responseDetail.Pet);
        Assert.True(agendaDto.PetOwner == responseDetail.PetOwner);
    }
}