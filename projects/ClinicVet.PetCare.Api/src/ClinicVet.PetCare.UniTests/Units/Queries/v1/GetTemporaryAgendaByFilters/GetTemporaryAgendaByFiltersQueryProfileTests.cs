using AutoMapper;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetTemporaryAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetTemporaryAgendaByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetTemporaryAgendaByFilters.Responses;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Queries.v1.GetTemporaryAgendaByFilters;

public sealed class GetTemporaryAgendaByFiltersQueryProfileTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetTemporaryAgendaByFiltersQueryProfile>());
        return config.CreateMapper();
    }

    [Fact(DisplayName = "Should map valid parameters in filters")]
    public void ShouldMapValidParametersInFilters()
    {
        var mapper = CreateMapper();
        var query = GetTemporaryAgendaByFiltersQueryMock.GetDefaultInstance();
        var filterDto = GetAgendaByFiltersDtoMock.GetDefaultInstance();

        mapper.Map(query, filterDto);

        Assert.True(query.Document == filterDto.Document);
        Assert.True(query.Skip == filterDto.Skip);
        Assert.True(query.Take == filterDto.Take);
    }

    [Fact(DisplayName = "Should maping return valid for temporary scheduling")]
    public void ShouldMapingReturnValidForTemporaryScheduling()
    {
        var mapper = CreateMapper();
        var agendaDto = AgendaDtoMock.GetDefaultInstance();
        var temporaryAgendaDto = TemporaryAgendaDtoMock.GetDefaultInstance();

        mapper.Map(temporaryAgendaDto, agendaDto);

        Assert.True(agendaDto.AppointmentAt == temporaryAgendaDto.AppointmentAt);
        Assert.True(agendaDto.Reason == temporaryAgendaDto.Reason);
        Assert.True(agendaDto.AgendaStatusType == temporaryAgendaDto.AgendaStatus);
        Assert.True(agendaDto.Pet!.Name == temporaryAgendaDto.PetName);
        Assert.True(agendaDto.Pet.Specie == temporaryAgendaDto.PetSpecie);
        Assert.True(agendaDto.PetOwner!.Contact!.Phone == temporaryAgendaDto.OwnerPhone);
        Assert.True(agendaDto.PetOwner.OwnerType == PetOwnerType.Permanent);
        Assert.True(agendaDto.PetOwner.Document!.Code == temporaryAgendaDto.OwnerDocument);
        Assert.True(agendaDto.PetOwner.Document!.Type == temporaryAgendaDto.DocType);
    }

    [Fact(DisplayName = "Should map valid parameters")]
    public void ShouldMapValidParameters()
    {
        var mapper = CreateMapper();
        var agendaDto = AgendaDtoMock.GetDefaultInstance();
        var responseDetail = GetTemporaryAgendaByFiltersQueryResponseDetailMock.GetDefaultInstance();

        mapper.Map(agendaDto, responseDetail);

        Assert.True(responseDetail.Pet == agendaDto.Pet);
        Assert.True(responseDetail.PetOwner == agendaDto.PetOwner);
    }
}