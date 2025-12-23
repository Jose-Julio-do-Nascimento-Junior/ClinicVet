using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

public sealed class PetMock
{
    public static Pet GetDefaultInstance()
    {
        return new Faker<Pet>(Constants.Language)
           .RuleFor(petMock => petMock.Name, fakerMock => fakerMock.Name.FirstName())
           .RuleFor(petMock => petMock.Specie, fakerMock => fakerMock.Random.String())
           .Generate();
    }
}