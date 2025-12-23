using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePet;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UnitTests.Mock.Dtos.v1.UpdatePet;

public sealed class UpdatePetDtoMock
{
    public static UpdatePetDto GetDefaultInstance()
    {
        return new Faker<UpdatePetDto>(Constants.Language)
           .RuleFor(petDto => petDto.Name, fakerMock => fakerMock.Name.FirstName())
           .RuleFor(petDto => petDto.Specie, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.Breed, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.BirthDate, DateTime.Now.ToString())
           .RuleFor(petDto => petDto.PetOwner, OwnerMock.GetDefaultInstance())
           .Generate();
    }
}