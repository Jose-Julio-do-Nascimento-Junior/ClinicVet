using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1;
using ClinicVet.PetCare.Domain.Resources.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetOwnerByFilters;
public sealed class PetOwnerByFiltersDtoMock
{
    public static PetOwnerByFiltersDto GetDefaultInstance()
    {
        return new Faker<PetOwnerByFiltersDto>(Constants.Language)
           .RuleFor(filterDto => filterDto.Name, fakerMock => fakerMock.Person.FirstName)
           .RuleFor(filterDto => filterDto.Document, fakerMock => fakerMock.Random.String())
           .RuleFor(filterDto => filterDto.Limit, fakerMock => fakerMock.Random.Int())
           .RuleFor(filterDto => filterDto.Offset, fakerMock => fakerMock.Random.Int())
           .Generate();
    }
}