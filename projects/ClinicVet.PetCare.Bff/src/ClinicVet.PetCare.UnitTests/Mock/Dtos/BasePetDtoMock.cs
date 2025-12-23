using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UnitTests.Mock.Dtos;

public sealed class BasePetDtoMock
{
    public static BasePetDto GetDefaultInstance()
    {
        return new Faker<BasePetDto>(Constants.Language)
           .RuleFor(petDto => petDto.Name, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.Specie, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.Breed, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.BirthDate, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.PetOwner, PetOwnerMock.GetDefaultInstance())
           .Generate();
    }
}