using Bogus;
using ClinicVet.PetCare.Domain.Commands.v1.UdpateAgenda;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Commands.v1.UdpateAgenda;

public sealed class UpdateAgendaCommandMock
{
    public static UpdateAgendaCommand GetDefaultInstance()
    {
        return new Faker<UpdateAgendaCommand>(Constants.Language)
           .RuleFor(command => command.AppointmentAt, DateTime.Now)
           .RuleFor(command => command.Reason, fakerMock => fakerMock.Random.String())
           .RuleFor(command => command.Status, AgendaStatusType.Scheduled)
           .RuleFor(command => command.Pet, PetMock.GetDefaultInstance())
           .RuleFor(command => command.PetOwner, PetOwnerMock.GetDefaultInstance())
           .Generate();
    }

    public static UpdateAgendaCommand GetNullInstance()
    {
        return new Faker<UpdateAgendaCommand>(Constants.Language)
           .RuleFor(command => command.Pet, PetMock.GetNullInstance())
           .RuleFor(command => command.PetOwner, PetOwnerMock.GetNullInstance())
           .Generate();
    }
}