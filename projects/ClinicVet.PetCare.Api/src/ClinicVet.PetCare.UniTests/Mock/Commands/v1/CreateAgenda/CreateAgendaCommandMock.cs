using Bogus;
using ClinicVet.PetCare.Domain.Commands.v1.CreateAgenda;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Commands.v1.CreateAgenda;

public sealed class CreateAgendaCommandMock
{
    public static CreateAgendaCommand GetDefaultInstance()
    {
        return new Faker<CreateAgendaCommand>(Constants.Language)
           .RuleFor(command => command.AppointmentAt, DateTime.Now)
           .RuleFor(command => command.Reason, fakerMock => fakerMock.Random.String())
           .RuleFor(command => command.Status, AgendaStatusType.Scheduled)
           .RuleFor(command => command.Pet, PetMock.GetDefaultInstance())
           .RuleFor(command => command.PetOwner, PetOwnerMock.GetDefaultInstance())
           .Generate();
    }

    public static CreateAgendaCommand GetNullInstance()
    {
        return new Faker<CreateAgendaCommand>(Constants.Language)
           .RuleFor(command => command.Pet, PetMock.GetNullInstance())
           .RuleFor(command => command.PetOwner, PetOwnerMock.GetNullInstance())
           .Generate();
    }
}