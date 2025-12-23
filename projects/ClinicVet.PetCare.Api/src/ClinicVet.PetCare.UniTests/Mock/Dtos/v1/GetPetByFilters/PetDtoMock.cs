using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetByFilters;

public sealed class PetDtoMock
{
    public static List<PetDto> GetDefaultInstances()
    {
        return new List<PetDto> { GetDefaultInstance() };
    }

    public static PetDto GetDefaultInstance()
    {
        return new Faker<PetDto>(Constants.Language)
           .RuleFor(petDto => petDto.Name, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.Specie, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.Breed, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.BirthDate, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.PetOwner, PetOwnerMock.GetDefaultInstance())
           .Generate();
    }

    public static List<PetDto> GetEmptyInstances() => new List<PetDto>();
}