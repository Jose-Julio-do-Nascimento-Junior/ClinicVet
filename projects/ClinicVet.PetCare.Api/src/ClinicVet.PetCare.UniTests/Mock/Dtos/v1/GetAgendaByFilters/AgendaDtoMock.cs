using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetAgendaByFilters;

public sealed class AgendaDtoMock
{
    public static List<AgendaDto> GetDefaultInstances()
    {
        return new List<AgendaDto> { GetDefaultInstance() };
    }

    public static AgendaDto GetDefaultInstance()
    {
        return new Faker<AgendaDto>(Constants.Language)
           .RuleFor(agendaDto => agendaDto.AppointmentAt, DateTime.Now.ToString())
           .RuleFor(agendaDto => agendaDto.Reason, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaDto => agendaDto.AgendaStatusType, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaDto => agendaDto.Pet, PetMock.GetDefaultInstance())
           .RuleFor(agendaDto => agendaDto.PetOwner, PetOwnerMock.GetDefaultInstance())
           .Generate();
    }

    public static List<AgendaDto> GetEmptyInstances() => new List<AgendaDto>();
}