using Bogus;
using ClinicVet.AgendaStatus.Job.Domain.Commands.v1.UpdateAgendaStatus;
using ClinicVet.AgendaStatus.Job.Domain.Resources.v1;

namespace ClinicVet.AgendaStatus.Job.UnitTests.Mock.Commands.v1.UpdateAgendaStatus;

public sealed class UpdateAgendaStatusCommandMock
{
    public static UpdateAgendaStatusCommand GetDefaultInstance()
        => new Faker<UpdateAgendaStatusCommand>(Constants.Language).Generate();
}