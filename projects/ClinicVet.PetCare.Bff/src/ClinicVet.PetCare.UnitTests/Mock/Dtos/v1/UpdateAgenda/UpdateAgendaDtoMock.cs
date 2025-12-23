using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdateAgenda;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UnitTests.Mock.Dtos.v1.UpdateAgenda;

public sealed class UpdateAgendaDtoMock
{
    public static UpdateAgendaDto GetDefaultInstance()
    {
        return new Faker<UpdateAgendaDto>(Constants.Language)
           .RuleFor(agendaParametersDto => agendaParametersDto.AppointmentAt, DateTime.Now)
           .RuleFor(agendaParametersDto => agendaParametersDto.Reason, fakerMock => fakerMock.Random.String())
           .RuleFor(agendaParametersDto => agendaParametersDto.Status, AgendaStatusType.Scheduled)
           .RuleFor(agendaParametersDto => agendaParametersDto.Pet, PetMock.GetDefaultInstance())
           .RuleFor(agendaParametersDto => agendaParametersDto.PetOwner, PetOwnerMock.GetDefaultInstance())
           .Generate();
    }
}