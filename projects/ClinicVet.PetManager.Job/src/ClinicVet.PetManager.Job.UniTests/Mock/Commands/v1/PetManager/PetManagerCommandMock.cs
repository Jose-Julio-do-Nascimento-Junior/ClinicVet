using Bogus;
using ClinicVet.PetManager.Job.Domain.Commands.v1.PetManager;
using ClinicVet.PetManager.Job.Domain.Resources.v1;

namespace ClinicVet.PetManager.Job.UniTests.Mock.Commands.v1.PetManager;

public sealed class PetManagerCommandMock
{
    public static PetManagerCommand GetDefaultInstance()
        => new Faker<PetManagerCommand>(Constants.Language).Generate();
}