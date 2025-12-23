using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UnitTests.Mock.Dtos;

public sealed class BaseAgendaDtoMock
{
    public static BaseAgendaDto GetDefaultInstance()
    {
        return new Faker<BaseAgendaDto>(Constants.Language)
           .RuleFor(agendaDto => agendaDto.AppointmentAt, DateTime.Now.ToString())
           .RuleFor(agendaDto => agendaDto.Reason, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaDto => agendaDto.AgendaStatusType, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaDto => agendaDto.Pet, PetMock.GetDefaultInstance())
           .RuleFor(agendaDto => agendaDto.PetOwner, PetOwnerMock.GetDefaultInstance())
           .Generate();
    }
}