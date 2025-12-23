using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.AgendaParameter;
using ClinicVet.PetCare.Domain.Resources.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Dtos.v1.AgendaParameter;

public sealed class AgendaParameterDtoMock
{
    public static AgendaParameterDto GetDefaultInstance()
    {
        return new Faker<AgendaParameterDto>(Constants.Language)
           .RuleFor(agendaParameterDto => agendaParameterDto.AppointmentAt, DateTime.Now)
           .RuleFor(agendaParameterDto => agendaParameterDto.Reason, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaParameterDto => agendaParameterDto.Status, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaParameterDto => agendaParameterDto.PetName, fakerMock => fakerMock.Name.FirstName())
           .RuleFor(agendaParameterDto => agendaParameterDto.PetSpecie, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaParameterDto => agendaParameterDto.OwnerDocument, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaParameterDto => agendaParameterDto.OwnerPhone, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaParameterDto => agendaParameterDto.OwnerType, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaParameterDto => agendaParameterDto.OwnerType, fakerMock => fakerMock.Random.String())
           .Generate();
    }
}