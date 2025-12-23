using Bogus;
using ClinicVet.PetCare.Domain.Commands.v1.CreatePet;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Commands.v1.CreatePet;

public sealed class CreatePetCommandMock
{
    public static CreatePetCommand GetDefaultInstance()
    {
        return new Faker<CreatePetCommand>(Constants.Language)
           .RuleFor(command => command.Name, fakerMock => fakerMock.Name.FirstName())
           .RuleFor(command => command.Specie, fakerMock => fakerMock.Random.String())
           .RuleFor(command => command.Breed, fakerMock => fakerMock.Random.String())
           .RuleFor(command => command.BirthDate, DateTime.Now.ToString())
           .RuleFor(command => command.PetOwner, OwnerMock.GetDefaultInstance())
           .Generate();
    }

    public static CreatePetCommand GetNullInstance()
    {
        return new Faker<CreatePetCommand>(Constants.Language)
           .RuleFor(command => command.PetOwner, OwnerMock.GetNullInstance())
           .Generate();
    }
}