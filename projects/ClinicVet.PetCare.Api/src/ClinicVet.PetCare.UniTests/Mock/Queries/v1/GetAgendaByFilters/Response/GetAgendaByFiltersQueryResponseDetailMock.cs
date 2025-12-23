using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters.Responses;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetAgendaByFilters.Response;

public sealed class GetAgendaByFiltersQueryResponseDetailMock
{
    public static List<GetAgendaByFiltersQueryResponseDetail> GetDefaultInstances()
    {
        return new List<GetAgendaByFiltersQueryResponseDetail> { GetDefaultInstance() };
    }

    public static GetAgendaByFiltersQueryResponseDetail GetDefaultInstance()
    {
        return new Faker<GetAgendaByFiltersQueryResponseDetail>(Constants.Language)
           .RuleFor(agendaDto => agendaDto.AppointmentAt, DateTime.Now.ToString())
           .RuleFor(agendaDto => agendaDto.Reason, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaDto => agendaDto.AgendaStatusType, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaDto => agendaDto.Pet, PetMock.GetDefaultInstance())
           .RuleFor(agendaDto => agendaDto.PetOwner, PetOwnerMock.GetDefaultInstance())
           .Generate();
    }
}