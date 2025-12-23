using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters.Responses;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetTemporaryAgendaByFilters.Responses;

public sealed class GetTemporaryAgendaByFiltersQueryResponseDetailMock
{
    public static List<GetTemporaryAgendaByFiltersQueryResponseDetail> GetDefaultInstances()
    {
        return new List<GetTemporaryAgendaByFiltersQueryResponseDetail> { GetDefaultInstance() };
    }

    public static GetTemporaryAgendaByFiltersQueryResponseDetail GetDefaultInstance()
    {
        return new Faker<GetTemporaryAgendaByFiltersQueryResponseDetail>(Constants.Language)
           .RuleFor(agendaDto => agendaDto.AppointmentAt, DateTime.Now.ToString())
           .RuleFor(agendaDto => agendaDto.Reason, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaDto => agendaDto.AgendaStatusType, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaDto => agendaDto.Pet, PetMock.GetDefaultInstance())
           .RuleFor(agendaDto => agendaDto.PetOwner, PetOwnerMock.GetDefaultInstance())
           .Generate();
    }
}